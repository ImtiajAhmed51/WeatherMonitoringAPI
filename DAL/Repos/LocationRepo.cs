using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    internal class LocationRepo : IRepo<Location, int, bool>
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


        public List<Location> GetByCountry(string country)
        {
            return db.Locations
                     .Where(l => l.Country.Equals(country, StringComparison.OrdinalIgnoreCase))
                     .ToList();
        }


        public List<Location> GetByName(string name)
        {
            return db.Locations
                     .Where(l => l.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                     .ToList();
        }
        public bool Exists(string name, string country)
        {
            return db.Locations.Any(l =>
                l.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                l.Country.Equals(country, StringComparison.OrdinalIgnoreCase));
        }

        public List<Location> GetWithActiveAlerts()
        {
            return db.Locations
                     .Where(l => l.Alerts.Any(a => a.IsActive))
                     .ToList();
        }
        public List<Location> GetNearby(decimal latitude, decimal longitude, double radiusKm)
        {
            //earth redius
            const double EarthRadius = 6378; 
            return db.Locations
                     .ToList()
                     .Where(l =>
                     {
                         var lat = (double)l.Latitude;
                         var lon = (double)l.Longitude;
                         var dLat = ToRadians(lat - (double)latitude);
                         var dLon = ToRadians(lon - (double)longitude);
                         var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                                 Math.Cos(ToRadians((double)latitude)) * Math.Cos(ToRadians(lat)) *
                                 Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                         var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                         var distance = EarthRadius * c;
                         return distance <= radiusKm;
                     }).ToList();
        }

        private double ToRadians(double deg)
        {
            return deg * Math.PI / 180;
        }
    }
}
