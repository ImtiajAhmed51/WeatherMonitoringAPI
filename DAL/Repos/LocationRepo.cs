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
            var var = Get(obj.Id);
            if (var == null)
                return false;

            db.Entry(var).CurrentValues.SetValues(obj);
            
            return db.SaveChanges()>0; ;
        }


        public List<Location> Search(string keyword)
        {
            return db.Locations
                     .Where(l => l.Name.ToLower().Contains(keyword.ToLower()) ||
                                 l.Country.ToLower().Contains(keyword.ToLower()))
                     .ToList();
        }

        public List<Location> GetWithActiveAlerts()
        {
            return db.Locations
                     .Where(l => l.Alerts.Any(a => a.IsActive))
                     .ToList();
        }
        public Location GetWithOtherData(int id)
        {
            return db.Locations
                     .Include(l => l.Alerts)        
                     .Include(l => l.WeatherRecords) 
                     .FirstOrDefault(l => l.Id == id);
        }
        public List<Location> GetLocationsInRange(double minLat, double maxLat, double minLon, double maxLon)
        {
            return db.Locations
                .Where(l => l.Latitude >= (decimal)minLat && l.Latitude <= (decimal)maxLat &&
                            l.Longitude >= (decimal)minLon && l.Longitude <= (decimal)maxLon)
                .ToList();
        }
    }
}
