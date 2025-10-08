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
        private const double EarthRadius = 6371;

        private static IMapper mapper;

        static LocationService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, LocationDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithAlertDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithWeatherRecordDTO>().ReverseMap();
                cfg.CreateMap<Location, LocationWithWeatherRecordAndAlertDTO>().ReverseMap();
                cfg.CreateMap<Alert, AlertDTO>().ReverseMap();
                cfg.CreateMap<WeatherRecord, WeatherRecordDTO>().ReverseMap();
                cfg.CreateMap<LocationWithWeatherRecordDTO, LocationWeatherRecordWithStatsDTO>().ReverseMap();
            });
            mapper = config.CreateMapper();

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
            if (DataAccessFactory.LocationDataFeature().LocationNameExists(dto.Name, dto.Country))
                return false;
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
        public static LocationWithAlertDTO GetLocationWithAlerts(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithAlerts(id);
            return location != null ? mapper.Map<LocationWithAlertDTO>(location) : null;
        }

        public static LocationWithWeatherRecordDTO GetLocationWithWeather(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithWeatherRecords(id);
            return location != null ? mapper.Map<LocationWithWeatherRecordDTO>(location) : null;
        }

        public static LocationWithWeatherRecordAndAlertDTO GetLocationWithWeatherAndAlerts(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithAllData(id);
            return location != null ? mapper.Map<LocationWithWeatherRecordAndAlertDTO>(location) : null;
        }

        public static List<LocationWithAlertDTO> GetAllLocationsWithAlerts()
        {
            var locations = DataAccessFactory.LocationDataFeature().GetAllWithAlerts();
            return mapper.Map<List<LocationWithAlertDTO>>(locations);
        }

        public static List<LocationWithWeatherRecordDTO> GetAllLocationsWithWeather()
        {
            var locations = DataAccessFactory.LocationDataFeature().GetAllWithWeatherRecords();
            return mapper.Map<List<LocationWithWeatherRecordDTO>>(locations);
        }
        public static List<LocationDTO> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<LocationDTO>();

            var locations = DataAccessFactory.LocationDataFeature().SearchByName(name);
            return mapper.Map<List<LocationDTO>>(locations);
        }

        public static List<LocationDTO> SearchByCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return new List<LocationDTO>();

            var locations = DataAccessFactory.LocationDataFeature().SearchByCountry(country);
            return mapper.Map<List<LocationDTO>>(locations);
        }

        public static List<LocationDTO> SearchByNameOrCountry(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<LocationDTO>();

            var byName = DataAccessFactory.LocationDataFeature().SearchByName(keyword);
            var byCountry = DataAccessFactory.LocationDataFeature().SearchByCountry(keyword);

            var combined = byName.Union(byCountry).Distinct().ToList();
            return mapper.Map<List<LocationDTO>>(combined);
        }

        public static LocationDTO GetByNameAndCountry(string name, string country)
        {
            var location = DataAccessFactory.LocationDataFeature().GetByNameAndCountry(name, country);
            return mapper.Map<LocationDTO>(location);
        }

        public static List<LocationDTO> GetByCoordinates(decimal latitude, decimal longitude)
        {
            var locations = DataAccessFactory.LocationDataFeature().GetByCoordinates(latitude, longitude);
            return mapper.Map<List<LocationDTO>>(locations);
        }
        public static List<LocationWithAlertDTO> GetLocationsWithActiveAlerts()
        {
            var locations = DataAccessFactory.LocationDataFeature().GetAllWithAlerts();
            var filtered = locations.Where(l => l.Alerts != null && l.Alerts.Any(a => a.IsActive)).ToList();
            return mapper.Map<List<LocationWithAlertDTO>>(filtered);
        }

        public static List<LocationDTO> GetLocationsWithActiveAlertsBasic()
        {
            var locations = DataAccessFactory.LocationDataFeature().GetAllWithAlerts();
            var filtered = locations.Where(l => l.Alerts != null && l.Alerts.Any(a => a.IsActive)).ToList();
            return mapper.Map<List<LocationDTO>>(filtered);
        }
        public static List<LocationDTO> GetNearbyLocations(decimal latitude, decimal longitude, double radiusKm)
        {
            return GetNearbyLocationsGeneric<LocationDTO>(latitude, longitude, radiusKm);
        }

        public static List<LocationWithAlertDTO> GetNearbyLocationsWithAlerts(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            var allLocations = DataAccessFactory.LocationDataFeature().GetAllWithAlerts();
            var nearby = allLocations
                .Where(l => CalculateDistance(lat, lon, (double)l.Latitude, (double)l.Longitude) <= radiusKm)
                .ToList();

            return mapper.Map<List<LocationWithAlertDTO>>(nearby);
        }

        public static List<LocationWithWeatherRecordDTO> GetNearbyLocationsWithWeather(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            var allLocations = DataAccessFactory.LocationDataFeature().GetAllWithWeatherRecords();
            var nearby = allLocations
                .Where(l => CalculateDistance(lat, lon, (double)l.Latitude, (double)l.Longitude) <= radiusKm)
                .ToList();

            return mapper.Map<List<LocationWithWeatherRecordDTO>>(nearby);
        }
        public static LocationDTO GetNearestLocation(decimal latitude, decimal longitude, double radiusKm)
        {
            return GetNearestLocationGeneric<LocationDTO>(latitude, longitude, radiusKm);
        }

        public static LocationWithAlertDTO GetNearestLocationWithAlerts(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            var allLocations = DataAccessFactory.LocationDataFeature().GetAllWithAlerts();

            Location nearest = null;
            double minDistance = double.MaxValue;

            foreach (var location in allLocations)
            {
                double distance = CalculateDistance(lat, lon, (double)location.Latitude, (double)location.Longitude);
                if (distance <= radiusKm && distance < minDistance)
                {
                    minDistance = distance;
                    nearest = location;
                }
            }

            return nearest != null ? mapper.Map<LocationWithAlertDTO>(nearest) : null;
        }
        public static LocationWithWeatherRecordDTO GetNearestLocationWithWeather(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            var allLocations = DataAccessFactory.LocationDataFeature().GetAllWithWeatherRecords();

            Location nearest = null;
            double minDistance = double.MaxValue;

            foreach (var location in allLocations)
            {
                double distance = CalculateDistance(lat, lon, (double)location.Latitude, (double)location.Longitude);
                if (distance <= radiusKm && distance < minDistance)
                {
                    minDistance = distance;
                    nearest = location;
                }
            }

            return nearest != null ? mapper.Map<LocationWithWeatherRecordDTO>(nearest) : null;
        }
        public static LocationWithWeatherRecordAndAlertDTO GetNearestLocationWithAll(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            var allLocations = DataAccessFactory.LocationData().Get();

            Location nearest = null;
            double minDistance = double.MaxValue;

            foreach (var location in allLocations)
            {
                double distance = CalculateDistance(lat, lon, (double)location.Latitude, (double)location.Longitude);
                if (distance <= radiusKm && distance < minDistance)
                {
                    minDistance = distance;
                    nearest = location;
                }
            }

            if (nearest == null)
                return null;

            var locationWithData = DataAccessFactory.LocationDataFeature().GetWithAllData(nearest.Id);
            return mapper.Map<LocationWithWeatherRecordAndAlertDTO>(locationWithData);
        }
        public static LocationWithWeatherRecordAndAlertDTO GetNearestWithActiveAlertsAndLatestWeather(decimal latitude, decimal longitude, double radiusKm)
        {
            var result = GetNearestLocationWithAll(latitude, longitude, radiusKm);

            if (result != null)
            {
                if (result.Alerts != null)
                {
                    result.Alerts = result.Alerts
                        .Where(a => a.IsActive && (a.ExpiresAt == null || a.ExpiresAt > DateTime.UtcNow))
                        .OrderByDescending(a => a.CreatedAt)
                        .ToList();
                }

                if (result.WeatherRecords != null && result.WeatherRecords.Any())
                {
                    result.WeatherRecords = result.WeatherRecords
                        .OrderByDescending(w => w.RecordedAt)
                        .Take(1)
                        .ToList();
                }
            }

            return result;
        }
        public static LocationWeatherRecordWithStatsDTO GetLocationWithWeatherStats(int id)
        {
            var location = DataAccessFactory.LocationDataFeature().GetWithWeatherRecords(id);
            if (location == null || location.WeatherRecords == null || !location.WeatherRecords.Any())
                return null;

            var locationDto = mapper.Map<LocationWithWeatherRecordDTO>(location);
            var result = mapper.Map<LocationWeatherRecordWithStatsDTO>(locationDto);

            result.DailyStats = locationDto.WeatherRecords
                .GroupBy(r => r.RecordedAt.Date)
                .Select(g => new DailyLocationWeatherStatsDTO
                {
                    Date = g.Key,
                    AverageTemperature = g.Average(r => (double)r.Temperature),
                    MinTemperature = g.Min(r => (double)r.Temperature),
                    MaxTemperature = g.Max(r => (double)r.Temperature),
                    AverageHumidity = g.Average(r => (double)r.Humidity),
                    TotalPrecipitation = g.Sum(r => (double)r.Precipitation),
                    TotalRecords = g.Count()
                })
                .OrderBy(s => s.Date)
                .ToList();

            return result;
        }
        public static LocationWeatherRecordWithStatsDTO GetNearestLocationWithWeatherStats(decimal latitude, decimal longitude, double radiusKm)
        {
            var nearestLocation = GetNearestLocationWithWeather(latitude, longitude, radiusKm);
            if (nearestLocation == null || nearestLocation.WeatherRecords == null || !nearestLocation.WeatherRecords.Any())
                return null;

            var result = mapper.Map<LocationWeatherRecordWithStatsDTO>(nearestLocation);

            result.DailyStats = nearestLocation.WeatherRecords
                .GroupBy(r => r.RecordedAt.Date)
                .Select(g => new DailyLocationWeatherStatsDTO
                {
                    Date = g.Key,
                    AverageTemperature = g.Average(r => (double)r.Temperature),
                    MinTemperature = g.Min(r => (double)r.Temperature),
                    MaxTemperature = g.Max(r => (double)r.Temperature),
                    AverageHumidity = g.Average(r => (double)r.Humidity),
                    TotalPrecipitation = g.Sum(r => (double)r.Precipitation),
                    TotalRecords = g.Count()
                })
                .OrderBy(s => s.Date)
                .ToList();

            return result;
        }
        public static int GetTotalLocationCount()
        {
            return DataAccessFactory.LocationDataFeature().GetLocationCount();
        }

        public static bool LocationExists(int id)
        {
            return DataAccessFactory.LocationDataFeature().Exists(id);
        }

        public static bool LocationNameExists(string name, string country)
        {
            return DataAccessFactory.LocationDataFeature().LocationNameExists(name, country);
        }

        public static Dictionary<string, int> GetLocationCountByCountry()
        {
            var locations = DataAccessFactory.LocationData().Get();
            return locations
                .GroupBy(l => l.Country)
                .ToDictionary(g => g.Key, g => g.Count());
        }
        private static List<T> GetNearbyLocationsGeneric<T>(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            var allLocations = DataAccessFactory.LocationData().Get();
            var nearby = allLocations
                .Where(l => CalculateDistance(lat, lon, (double)l.Latitude, (double)l.Longitude) <= radiusKm)
                .ToList();

            return mapper.Map<List<T>>(nearby);
        }

        private static T GetNearestLocationGeneric<T>(decimal latitude, decimal longitude, double radiusKm)
        {
            double lat = (double)latitude;
            double lon = (double)longitude;

            var allLocations = DataAccessFactory.LocationData().Get();

            Location nearest = null;
            double minDistance = double.MaxValue;

            foreach (var location in allLocations)
            {
                double distance = CalculateDistance(lat, lon, (double)location.Latitude, (double)location.Longitude);
                if (distance <= radiusKm && distance < minDistance)
                {
                    minDistance = distance;
                    nearest = location;
                }
            }

            return nearest != null ? mapper.Map<T>(nearest) : default(T);
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

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
        public static bool IsLocationValid(LocationDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return false;
            if (string.IsNullOrWhiteSpace(dto.Country))
                return false;
            if (dto.Latitude < -90 || dto.Latitude > 90)
                return false;
            if (dto.Longitude < -180 || dto.Longitude > 180)
                return false;
            return true;
        }
    }
}