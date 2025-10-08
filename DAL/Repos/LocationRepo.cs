using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DAL.Repos
{
    internal class LocationRepo : IRepo<Location, int, bool>, ILocationRepo
    {
        private readonly WeatherContext db;

        public LocationRepo()
        {
            db = WeatherContextSingleton.GetInstance();
        }
        public bool Create(Location obj)
        {
            db.Locations.Add(obj);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var location = Get(id);
            if (location == null)
                return false;

            db.Locations.Remove(location);
           
            return db.SaveChanges()>0;
        }
        public List<Location> Get()
        {
            return db.Locations.ToList();
        }
        public Location Get(int id)
        {
            return db.Locations.Find(id);
        }
        public bool Update(Location obj)
        {
            var data = Get(obj.Id);
            if (data == null)
                return false;

            db.Entry(data).CurrentValues.SetValues(obj);
            
            return db.SaveChanges()>0; ;
        }
        public Location GetWithAlerts(int id)
        {
            return db.Locations
                     .Include(l => l.Alerts)
                     .FirstOrDefault(l => l.Id == id);
        }

        public Location GetWithWeatherRecords(int id)
        {
            return db.Locations
                     .Include(l => l.WeatherRecords)
                     .FirstOrDefault(l => l.Id == id);
        }

        public Location GetWithAllData(int id)
        {
            return db.Locations
                     .Include(l => l.Alerts)
                     .Include(l => l.WeatherRecords)
                     .FirstOrDefault(l => l.Id == id);
        }
        public List<Location> GetAllWithAlerts()
        {
            return db.Locations
                     .Include(l => l.Alerts)
                     .ToList();
        }
        public List<Location> GetAllWithWeatherRecords()
        {
            return db.Locations
                     .Include(l => l.WeatherRecords)
                     .ToList();
        }

        public List<Location> SearchByName(string name)
        {
            return db.Locations
                     .Where(l => l.Name.Contains(name))
                     .ToList();
        }

        public List<Location> SearchByCountry(string country)
        {
            return db.Locations
                     .Where(l => l.Country.Contains(country))
                     .ToList();
        }

        public Location GetByNameAndCountry(string name, string country)
        {
            return db.Locations
                     .FirstOrDefault(l => l.Name == name && l.Country == country);
        }

        public List<Location> GetByCoordinates(decimal latitude, decimal longitude)
        {
            return db.Locations
                     .Where(l => l.Latitude == latitude && l.Longitude == longitude)
                     .ToList();
        }
        public int GetLocationCount()
        {
            return db.Locations.Count();
        }

        public bool Exists(int id)
        {
            return db.Locations.Any(l => l.Id == id);
        }

        public bool LocationNameExists(string name, string country)
        {
            return db.Locations.Any(l => l.Name == name && l.Country == country);
        }
    }
}
