using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class WeatherRecordService
    {
        private static IMapper mapper;


        static WeatherRecordService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WeatherRecord, WeatherRecordDTO>().ReverseMap();

                cfg.CreateMap<WeatherRecord, WeatherRecordWithLocationDTO>().ReverseMap();

                cfg.CreateMap<Location, LocationDTO>().ReverseMap();
            });
            mapper=config.CreateMapper();
        }

        public static List<WeatherRecordDTO> GetAllWeatherRecords()
        {
            var records = DataAccessFactory.WeatherRecordData().Get();
            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static WeatherRecordDTO GetWeatherRecordById(int id)
        {
            var record = DataAccessFactory.WeatherRecordData().Get(id);
            return mapper.Map<WeatherRecordDTO>(record);
        }

        public static bool CreateWeatherRecord(WeatherRecordDTO dto)
        {
            if (!IsWeatherRecordValid(dto))
                return false;
            if (DataAccessFactory.WeatherRecordDataFeature().RecordExistsForLocation(dto.LocationId, dto.RecordedAt))
                return false;

            var entity = mapper.Map<WeatherRecord>(dto);
            return DataAccessFactory.WeatherRecordData().Create(entity);
        }

        public static bool UpdateWeatherRecord(WeatherRecordDTO dto)
        {
            if (!IsWeatherRecordValid(dto))
                return false;

            var entity = mapper.Map<WeatherRecord>(dto);
            return DataAccessFactory.WeatherRecordData().Update(entity);
        }

        public static bool DeleteWeatherRecord(int id)
        {
            return DataAccessFactory.WeatherRecordData().Delete(id);
        }

        public static WeatherRecordWithLocationDTO GetWeatherRecordWithLocation(int id)
        {
            var record = DataAccessFactory.WeatherRecordDataFeature().GetWithLocation(id);
            return mapper.Map<WeatherRecordWithLocationDTO>(record);
        }

        public static List<WeatherRecordWithLocationDTO> GetAllWeatherRecordsWithLocations()
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetAllWithLocations();
            return mapper.Map<List<WeatherRecordWithLocationDTO>>(records);
        }
        public static List<WeatherRecordDTO> GetWeatherRecordsByLocation(int locationId)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetByLocation(locationId);
            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static List<WeatherRecordWithLocationDTO> GetWeatherRecordsByLocationWithDetails(int locationId)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetByLocationWithLocation(locationId);
            return mapper.Map<List<WeatherRecordWithLocationDTO>>(records);
        }

        public static WeatherRecordDTO GetLatestWeatherRecordByLocation(int locationId)
        {
            var record = DataAccessFactory.WeatherRecordDataFeature().GetLatestByLocation(locationId);
            return mapper.Map<WeatherRecordDTO>(record);
        }

        public static WeatherRecordWithLocationDTO GetLatestWeatherRecordByLocationWithDetails(int locationId)
        {
            var record = DataAccessFactory.WeatherRecordDataFeature().GetLatestByLocation(locationId);
            if (record == null)
                return null;

            var recordWithLocation = DataAccessFactory.WeatherRecordDataFeature().GetWithLocation(record.Id);
            return mapper.Map<WeatherRecordWithLocationDTO>(recordWithLocation);
        }
        public static List<WeatherRecordDTO> GetWeatherRecordsByDateRange(DateTime start, DateTime end)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetByDateRange(start, end);
            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static List<WeatherRecordWithLocationDTO> GetWeatherRecordsByDateRangeWithLocations(DateTime start, DateTime end)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetByDateRange(start, end);
            var recordIds = records.Select(r => r.Id).ToList();

            var recordsWithLocations = DataAccessFactory.WeatherRecordDataFeature().GetAllWithLocations()
                .Where(r => recordIds.Contains(r.Id))
                .ToList();

            return mapper.Map<List<WeatherRecordWithLocationDTO>>(recordsWithLocations);
        }

        public static List<WeatherRecordDTO> GetWeatherRecordsByLocationAndDateRange(int locationId, DateTime start, DateTime end)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetByLocationAndDateRange(locationId, start, end);
            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static List<WeatherRecordDTO> GetWeatherRecordsAfter(DateTime date)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetRecordedAfter(date);
            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static List<WeatherRecordDTO> GetWeatherRecordsBefore(DateTime date)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetRecordedBefore(date);
            return mapper.Map<List<WeatherRecordDTO>>(records);
        }
        public static List<WeatherRecordDTO> GetRecentWeatherRecords(int count = 10)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetRecentRecords(count);
            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static List<WeatherRecordWithLocationDTO> GetRecentWeatherRecordsWithLocations(int count = 10)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetRecentRecords(count);
            var recordIds = records.Select(r => r.Id).ToList();

            var recordsWithLocations = DataAccessFactory.WeatherRecordDataFeature().GetAllWithLocations()
                .Where(r => recordIds.Contains(r.Id))
                .OrderByDescending(r => r.RecordedAt)
                .ToList();

            return mapper.Map<List<WeatherRecordWithLocationDTO>>(recordsWithLocations);
        }

        public static List<WeatherRecordDTO> GetRecentWeatherRecordsByLocation(int locationId, int count = 10)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetRecentRecordsByLocation(locationId, count);
            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static List<WeatherRecordWithLocationDTO> GetLatestWeatherRecordsForAllLocations()
        {
            var allRecords = DataAccessFactory.WeatherRecordDataFeature().GetAllWithLocations();

            var latestRecords = allRecords
                .GroupBy(r => r.LocationId)
                .Select(g => g.OrderByDescending(r => r.RecordedAt).FirstOrDefault())
                .Where(r => r != null)
                .ToList();

            return mapper.Map<List<WeatherRecordWithLocationDTO>>(latestRecords);
        }

        public static List<WeatherRecordDTO> GetWeatherRecordsByTemperature(decimal min, decimal max)
        {
            var records = DataAccessFactory.WeatherRecordData().Get()
                .Where(r => r.Temperature >= min && r.Temperature <= max)
                .OrderByDescending(r => r.RecordedAt)
                .ToList();

            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static List<WeatherRecordWithLocationDTO> GetWeatherRecordsByTemperatureWithLocations(decimal min, decimal max)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetAllWithLocations()
                .Where(r => r.Temperature >= min && r.Temperature <= max)
                .OrderByDescending(r => r.RecordedAt)
                .ToList();

            return mapper.Map<List<WeatherRecordWithLocationDTO>>(records);
        }

        public static List<WeatherRecordDTO> GetWeatherRecordsByHumidity(decimal min, decimal max)
        {
            var records = DataAccessFactory.WeatherRecordData().Get()
                .Where(r => r.Humidity >= min && r.Humidity <= max)
                .OrderByDescending(r => r.RecordedAt)
                .ToList();

            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static List<WeatherRecordWithLocationDTO> GetWeatherRecordsByHumidityWithLocations(decimal min, decimal max)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetAllWithLocations()
                .Where(r => r.Humidity >= min && r.Humidity <= max)
                .OrderByDescending(r => r.RecordedAt)
                .ToList();

            return mapper.Map<List<WeatherRecordWithLocationDTO>>(records);
        }

        public static List<WeatherRecordDTO> GetWeatherRecordsByPrecipitation(decimal min, decimal max)
        {
            var records = DataAccessFactory.WeatherRecordData().Get()
                .Where(r => r.Precipitation >= min && r.Precipitation <= max)
                .OrderByDescending(r => r.RecordedAt)
                .ToList();

            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static List<WeatherRecordDTO> GetWeatherRecordsByWindSpeed(decimal min, decimal max)
        {
            var records = DataAccessFactory.WeatherRecordData().Get()
                .Where(r => r.WindSpeed >= min && r.WindSpeed <= max)
                .OrderByDescending(r => r.RecordedAt)
                .ToList();

            return mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public static int GetTotalRecordCount()
        {
            return DataAccessFactory.WeatherRecordDataFeature().GetTotalRecordCount();
        }

        public static int GetRecordCountByLocation(int locationId)
        {
            return DataAccessFactory.WeatherRecordDataFeature().GetRecordCountByLocation(locationId);
        }

        public static Dictionary<int, int> GetRecordCountsByLocation()
        {
            var records = DataAccessFactory.WeatherRecordData().Get();
            return records
                .GroupBy(r => r.LocationId)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public static DateTime? GetFirstRecordDate(int locationId)
        {
            return DataAccessFactory.WeatherRecordDataFeature().GetFirstRecordDate(locationId);
        }

        public static DateTime? GetLastRecordDate(int locationId)
        {
            return DataAccessFactory.WeatherRecordDataFeature().GetLastRecordDate(locationId);
        }

        public static WeatherStatsDTO GetWeatherStatsByLocation(int locationId)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetByLocation(locationId);

            if (records == null || records.Count == 0)
                return null;

            return new WeatherStatsDTO
            {
                LocationId = locationId,
                TotalRecords = records.Count,
                AverageTemperature = records.Average(r=>(double)r.Temperature),
                MinTemperature = records.Min(r =>(double)r.Temperature),
                MaxTemperature = records.Max(r => (double)r.Temperature),
                AverageHumidity = records.Average(r => (double)r.Humidity),
                MinHumidity = records.Min(r => (double)r.Humidity),
                MaxHumidity = records.Max(r => (double)r.Humidity),
                AveragePrecipitation = records.Average(r => (double)r.Precipitation),
                TotalPrecipitation = records.Sum(r => (double)r.Precipitation),
                AverageWindSpeed = records.Average(r => (double)r.WindSpeed),
                MaxWindSpeed = records.Max(r => (double)r.WindSpeed),
                FirstRecordDate = records.Min(r => r.RecordedAt),
                LastRecordDate = records.Max(r => r.RecordedAt)
            };
        }

        public static WeatherStatsDTO GetWeatherStatsByLocationAndDateRange(int locationId, DateTime start, DateTime end)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature()
                .GetByLocationAndDateRange(locationId, start, end);

            if (records == null || records.Count == 0)
                return null;

            return new WeatherStatsDTO
            {
                LocationId = locationId,
                TotalRecords = records.Count,
                AverageTemperature = records.Average(r => (double)r.Temperature),
                MinTemperature = records.Min(r => (double)r.Temperature),
                MaxTemperature = records.Max(r => (double)r.Temperature),
                AverageHumidity = records.Average(r => (double)r.Humidity),
                MinHumidity = records.Min(r => (double)r.Humidity),
                MaxHumidity = records.Max(r => (double)r.Humidity),
                AveragePrecipitation = records.Average(r => (double)r.Precipitation),
                TotalPrecipitation = records.Sum(r => (double)r.Precipitation),
                AverageWindSpeed = records.Average(r => (double)r.WindSpeed),
                MaxWindSpeed = records.Max(r => (double)r.WindSpeed),
                FirstRecordDate = records.Min(r => r.RecordedAt),
                LastRecordDate = records.Max(r => r.RecordedAt)
            };
        }
        public static bool WeatherRecordExists(int id)
        {
            return DataAccessFactory.WeatherRecordDataFeature().Exists(id);
        }
        public static bool RecordExistsForLocation(int locationId, DateTime recordedAt)
        {
            return DataAccessFactory.WeatherRecordDataFeature().RecordExistsForLocation(locationId, recordedAt);
        }
        public static bool IsWeatherRecordValid(WeatherRecordDTO dto)
        {
            if (dto.LocationId <= 0)
                return false;
            if (dto.Temperature < -100 || dto.Temperature > 100)
                return false;
            if (dto.Humidity < 0 || dto.Humidity > 100)
                return false;
            if (dto.Precipitation < 0)
                return false;
            if (dto.WindSpeed < 0)
                return false;
            if (dto.RecordedAt > DateTime.UtcNow)
                return false;
            return true;
        }

    }
}
