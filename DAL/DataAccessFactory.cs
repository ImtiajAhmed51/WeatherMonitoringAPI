using DAL.Factory;
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
    public sealed class DataAccessFactory
    {

        private static readonly Lazy<DataAccessFactory> _instance =
            new Lazy<DataAccessFactory>(() => new DataAccessFactory());

        private DataAccessFactory() { }

        public static DataAccessFactory Instance => _instance.Value;

        public IRepoFacade CreateFacade()
        {
            return new RepoFacade();
        }

        public IRepoFacade CreateFacade(WeatherContext context)
        {
            return new RepoFacade(context);
        }








        // later you need to fix the following codes
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
        public static IAlertRepo AlertDataFeature()
        {
            return new AlertRepo();
        }
        public static IWeatherRecordRepo WeatherRecordDataFeature()
        {
            return new WeatherRecordRepo();
        }

    }
}
