using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class WeatherRecordService
    {
        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WeatherRecord, WeatherRecordDTO>().ReverseMap();

                cfg.CreateMap<WeatherRecord, WeatherRecordWithLocationDTO>().ReverseMap();

                cfg.CreateMap<Location, LocationDTO>().ReverseMap();
            });

            return new Mapper(config);
        }

        public static List<WeatherRecordDTO> GetAllWeatherRecords()
        {
            var records = DataAccessFactory.WeatherRecordData().Get();
            return GetMapper().Map<List<WeatherRecordDTO>>(records);
        }

        public static WeatherRecordDTO GetWeatherRecordById(int id)
        {
            var record = DataAccessFactory.WeatherRecordData().Get(id);
            return GetMapper().Map<WeatherRecordDTO>(record);
        }

        public static bool CreateWeatherRecord(WeatherRecordDTO dto)
        {
            var entity = GetMapper().Map<WeatherRecord>(dto);
            return DataAccessFactory.WeatherRecordData().Create(entity);
        }

        public static bool UpdateWeatherRecord(WeatherRecordDTO dto)
        {
            var entity = GetMapper().Map<WeatherRecord>(dto);
            return DataAccessFactory.WeatherRecordData().Update(entity);
        }

        public static bool DeleteWeatherRecord(int id)
        {
            return DataAccessFactory.WeatherRecordData().Delete(id);
        }

        public static List<WeatherRecordWithLocationDTO> GetWeatherRecordsByLocation(int locationId)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetByLocation(locationId);
            return GetMapper().Map<List<WeatherRecordWithLocationDTO>>(records);
        }

        public static List<WeatherRecordWithLocationDTO> GetWeatherRecordsByDateRange(DateTime start, DateTime end)
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetByDateRange(start, end);
            return GetMapper().Map<List<WeatherRecordWithLocationDTO>>(records);
        }

        public static List<WeatherRecordWithLocationDTO> GetLatestWeatherRecords()
        {
            var records = DataAccessFactory.WeatherRecordDataFeature().GetLatestRecords();
            return GetMapper().Map<List<WeatherRecordWithLocationDTO>>(records);
        }
    }
}
