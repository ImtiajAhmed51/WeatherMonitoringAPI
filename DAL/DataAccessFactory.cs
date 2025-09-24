using DAL.Interfaces;
using DAL.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<Location, int, bool> LocationData()
        {
            return new LocationRepo();
        }
        public static IRepo<Alert, int, bool> AlertData()
        {
            return new AlertRepo();
        }
        public static IRepo<WeatherRecord, int, bool> WeatherRecordData()
        {
            return new WeatherRecordRepo();
        }
        public static ILocationRepo LocationDataFeature()
        {
            return new LocationRepo();
        }
        public static IWeatherRecordRepo WeatherRecordDataFeature()
        {
            return new WeatherRecordRepo();
        }

    }
}
