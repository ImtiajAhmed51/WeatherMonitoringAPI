using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class LocationService
    {
        private const double EarthRadius = 6378;
        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, LocationDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithAlertsDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithWeatherRecordDTO>().ReverseMap();
                cfg.CreateMap<Alert, AlertDTO>().ReverseMap();
                cfg.CreateMap<WeatherRecord, WeatherRecordDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithWeatherRecrodAndAlertDTO>()
                .ReverseMap();  
                cfg.CreateMap<Location, LocationWithWeatherRecrodAndAlertDTO>().ReverseMap();
            });

            return new Mapper(config);
        }

        public static List<LocationDTO> GetAllLocations()
        {
            var locations = DataAccessFactory.LocationData().Get();
            return GetMapper().Map<List<LocationDTO>>(locations);
        }

        public static LocationDTO GetLocationById(int id)
        {
            var location = DataAccessFactory.LocationData().Get(id);
            return GetMapper().Map<LocationDTO>(location);
        }

        public static bool CreateLocation(LocationDTO dto)
        {
            var entity = GetMapper().Map<Location>(dto);
            return DataAccessFactory.LocationData().Create(entity);
        }

        public static bool UpdateLocation(LocationDTO dto)
        {
            var entity = GetMapper().Map<Location>(dto);
            return DataAccessFactory.LocationData().Update(entity);
        }

        public static bool DeleteLocation(int id)
        {
            return DataAccessFactory.LocationData().Delete(id);
        }


        public static List<LocationDTO> SearchLocationsByNameOrCountry(string name)
        {
            var locations = DataAccessFactory.LocationDataFeature().Search(name);
            return GetMapper().Map<List<LocationDTO>>(locations);
        }


        public static List<LocationWithAlertsDTO> GetLocationsWithActiveAlerts()
        {
            var locations = DataAccessFactory.LocationDataFeature().GetWithActiveAlerts();
            return GetMapper().Map<List<LocationWithAlertsDTO>>(locations);
        }





        public static List<LocationWithWeatherRecordDTO> GetNearbyLocations(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            double latRadius = radiusKm / EarthRadius * (180 / Math.PI);
            double lonRadius = radiusKm / (EarthRadius * Math.Cos(lat * Math.PI / 180)) * (180 / Math.PI);

            double minLat = lat - latRadius;
            double maxLat = lat + latRadius;
            double minLon = lon - lonRadius;
            double maxLon = lon + lonRadius;

            var candidates = DataAccessFactory.LocationDataFeature().GetLocationsInRange(minLat, maxLat, minLon, maxLon);
            var nearby = candidates
                .Where(l => CalculateDistance(lat, lon, (double)l.Latitude, (double)l.Longitude) <= radiusKm)
                .ToList();

            return GetMapper().Map<List<LocationWithWeatherRecordDTO>>(nearby);
        }

        public static LocationWithWeatherRecordDTO GetNearestLocationWeatherRecords(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            double latRadius = radiusKm / EarthRadius * (180 / Math.PI);
            double lonRadius = radiusKm / (EarthRadius * Math.Cos(lat * Math.PI / 180)) * (180 / Math.PI);

            double minLat = lat - latRadius;
            double maxLat = lat + latRadius;
            double minLon = lon - lonRadius;
            double maxLon = lon + lonRadius;

            var candidates = DataAccessFactory.LocationDataFeature().GetLocationsInRange(minLat, maxLat, minLon, maxLon);

            Location nearest = null;
            double minDistance = double.MaxValue;

            foreach (var l in candidates)
            {
                double distance = CalculateDistance(lat, lon, (double)l.Latitude, (double)l.Longitude);
                if (distance <= radiusKm && distance < minDistance)
                {
                    minDistance = distance;
                    nearest = l;
                }
            }

            return nearest!=null? GetMapper().Map<LocationWithWeatherRecordDTO>(nearest):null;
        }


        public static LocationWithWeatherRecordDTO GetLocationWithWeather(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithOtherData(id); 
            if (location == null) 
                return null;
            return GetMapper().Map<LocationWithWeatherRecordDTO>(location);
        }

        public static LocationWithAlertsDTO GetLocationWithAlerts(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithOtherData(id);
            if (location == null) return null;
            return GetMapper().Map<LocationWithAlertsDTO>(location);
        }

        public static LocationWithWeatherRecrodAndAlertDTO GetLocationWithWeatherAndAlerts(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithOtherData(id); 
            if (location == null) return null;
            return GetMapper().Map<LocationWithWeatherRecrodAndAlertDTO>(location);
        }

        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadius * c;
        }


        private static double ToRadians(double deg)
        {
            return deg * Math.PI / 180;
        }
    }
}
