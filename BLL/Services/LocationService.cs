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

        private static IMapper mapper;

        static LocationService() {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, LocationDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithAlertDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithWeatherRecordDTO>().ReverseMap();
                cfg.CreateMap<Alert, AlertDTO>().ReverseMap();
                cfg.CreateMap<WeatherRecord, WeatherRecordDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithWeatherRecordAndAlertDTO>()
                .ReverseMap();
            });
            mapper=config.CreateMapper();

        }

        public static List<LocationDTO> GetAllLocations()
        {
            var locations = DataAccessFactory.LocationData().Get();
            return mapper.Map<List<LocationDTO>>(locations);
        }

        public static LocationDTO GetLocationById(int id)
        {
            var location = DataAccessFactory.LocationData().Get(id);
            return mapper.Map<LocationDTO>(location);
        }

        public static bool CreateLocation(LocationDTO dto)
        {
            var entity = mapper.Map<Location>(dto);
            return DataAccessFactory.LocationData().Create(entity);
        }

        public static bool UpdateLocation(LocationDTO dto)
        {
            var entity = mapper.Map<Location>(dto);
            return DataAccessFactory.LocationData().Update(entity);
        }

        public static bool DeleteLocation(int id)
        {
            return DataAccessFactory.LocationData().Delete(id);
        }


        public static List<LocationDTO> SearchLocationsByNameOrCountry(string name)
        {
            var locations = DataAccessFactory.LocationDataFeature().Search(name);
            return mapper.Map<List<LocationDTO>>(locations);
        }


        public static List<LocationWithAlertDTO> GetLocationsWithActiveAlerts()
        {
            var locations = DataAccessFactory.LocationDataFeature().GetWithActiveAlerts();
            return mapper.Map<List<LocationWithAlertDTO>>(locations);
        }


        public static List<T> GetNearbyLocationsGeneric<T>(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            double latRadius = ToLatRadius(radiusKm);
            double lonRadius = ToLonRadius(radiusKm, lat);

            double minLat = lat - latRadius;
            double maxLat = lat + latRadius;
            double minLon = lon - lonRadius;
            double maxLon = lon + lonRadius;

            var candidates = DataAccessFactory.LocationDataFeature()
                                .GetLocationsInRange(minLat, maxLat, minLon, maxLon);

            var nearby = candidates
                .Where(l => CalculateDistance(lat, lon, (double)l.Latitude, (double)l.Longitude) <= radiusKm)
                .ToList();

            return mapper.Map<List<T>>(nearby);
        }

        public static List<LocationDTO> GetNearbyLocations(decimal latitude, decimal longitude, double radiusKm)
        {

            return GetNearbyLocationsGeneric<LocationDTO>(latitude, longitude, radiusKm);
        }


        public static List<LocationWithWeatherRecordDTO> GetNearbyLocationsWeatherRecord(decimal latitude, decimal longitude, double radiusKm)
        {
            return GetNearbyLocationsGeneric<LocationWithWeatherRecordDTO>(latitude, longitude, radiusKm);
        }


        public static List<LocationWithAlertDTO> GetNearbyLocationsAlert(decimal latitude, decimal longitude, double radiusKm)
        {
            return GetNearbyLocationsGeneric<LocationWithAlertDTO>(latitude, longitude, radiusKm);
        }

        public static List<LocationWithWeatherRecordAndAlertDTO> GetNearbyLocationsWeatherRecordAndAlert(decimal latitude, decimal longitude, double radiusKm)
        {
            return GetNearbyLocationsGeneric<LocationWithWeatherRecordAndAlertDTO>(latitude, longitude, radiusKm);
        }


        public static T GetNearestbyLocationsGeneric<T>(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            double latRadius = ToLatRadius(radiusKm);
            double lonRadius = ToLonRadius(radiusKm, lat);

            double minLat = lat - latRadius;
            double maxLat = lat + latRadius;
            double minLon = lon - lonRadius;
            double maxLon = lon + lonRadius;

            var locations = DataAccessFactory.LocationDataFeature().GetLocationsInRange(minLat, maxLat, minLon, maxLon);

            Location nearest = null;
            double minDistance = double.MaxValue;

            foreach (var l in locations)
            {
                double distance = CalculateDistance(lat, lon, (double)l.Latitude, (double)l.Longitude);
                if (distance <= radiusKm && distance < minDistance)
                {
                    minDistance = distance;
                    nearest = l;
                }
            }

            return nearest !=null ?mapper.Map<T>(nearest):default;
        }
        public static LocationDTO GetNearest(decimal latitude, decimal longitude, double radiusKm)
        {


            return GetNearestbyLocationsGeneric<LocationDTO>(latitude, longitude, radiusKm) != null ? GetNearestbyLocationsGeneric<LocationDTO>(latitude, longitude, radiusKm) : null;
        }


        public static LocationWithWeatherRecordDTO GetNearestWeatherRecords(decimal latitude, decimal longitude, double radiusKm)
        {
            

            return GetNearestbyLocationsGeneric<LocationWithWeatherRecordDTO>(latitude, longitude, radiusKm)!=null? GetNearestbyLocationsGeneric<LocationWithWeatherRecordDTO>(latitude, longitude, radiusKm):null ;
        }
      


        public static LocationWithAlertDTO GetNearestAlerts(decimal latitude, decimal longitude, double radiusKm)
        {


            return GetNearestbyLocationsGeneric<LocationWithAlertDTO>(latitude, longitude, radiusKm);
        }

        public static LocationWithWeatherRecordAndAlertDTO GetNearestWeatherRecordsAlerts(decimal latitude, decimal longitude, double radiusKm)
        {


            return GetNearestbyLocationsGeneric<LocationWithWeatherRecordAndAlertDTO>(latitude, longitude, radiusKm);
        }

        public static LocationWithWeatherRecordDTO GetLocationWithWeather(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithOtherData(id); 
            return location != null? mapper.Map<LocationWithWeatherRecordDTO>(location):null;
        }

        public static LocationWithAlertDTO GetLocationWithAlerts(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithOtherData(id);
   
            return location != null? mapper.Map<LocationWithAlertDTO>(location):null;
        }

        public static LocationWithWeatherRecordAndAlertDTO GetLocationWithWeatherAndAlerts(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithOtherData(id); 

            return location != null? mapper.Map<LocationWithWeatherRecordAndAlertDTO>(location):null;
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

        private static double ToLatRadius(double radiusKm)
        {

            return radiusKm / EarthRadius * (180 / Math.PI);
        }
        private static double ToLonRadius(double radiusKm,double lat)
        {
            return radiusKm / (EarthRadius * Math.Cos(lat * Math.PI / 180)) * (180 / Math.PI);
        }


        private static double ToRadians(double deg)
        {
            return deg * Math.PI / 180;
        }
    }
}
