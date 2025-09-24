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
        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, LocationDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithAlertsDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithWeatherRecordDTO>().ReverseMap();
                cfg.CreateMap<Alert, AlertDTO>().ReverseMap();
                cfg.CreateMap<WeatherRecord, WeatherRecordDTO>().ReverseMap();
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

        public static List<LocationDTO> GetLocationsByCountry(string country)
        {
            var locations = DataAccessFactory.LocationDataFeature().GetByCountry(country);
            return GetMapper().Map<List<LocationDTO>>(locations);
        }

        public static List<LocationDTO> SearchLocationsByName(string name)
        {
            var locations = DataAccessFactory.LocationDataFeature().GetByName(name);
            return GetMapper().Map<List<LocationDTO>>(locations);
        }

        public static bool CheckLocationExists(string name, string country)
        {
            return DataAccessFactory.LocationDataFeature().Exists(name, country);
        }

        public static List<LocationWithAlertsDTO> GetLocationsWithActiveAlerts()
        {
            var locations = DataAccessFactory.LocationDataFeature().GetWithActiveAlerts();
            return GetMapper().Map<List<LocationWithAlertsDTO>>(locations);
        }

        public static List<LocationDTO> GetNearbyLocations(decimal latitude, decimal longitude, double radiusKm)
        {
            var locations = DataAccessFactory.LocationDataFeature().GetNearby(latitude, longitude, radiusKm);
            return GetMapper().Map<List<LocationDTO>>(locations);
        }

        public static LocationWithWeatherRecordDTO GetNearestLocationWeatherRecords(decimal latitude, decimal longitude, double radiusKm)
        {
            // nearest location fetch
            var nearestLocation = DataAccessFactory.LocationDataFeature().GetNearest(latitude, longitude, radiusKm);
            if (nearestLocation == null)
                return null;

            // AutoMapper দিয়ে map
            var mapper = GetMapper();
            return mapper.Map<LocationWithWeatherRecordDTO>(nearestLocation);
        }
    }
}
