using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class LocationRepo : IRepo<Location, int, bool>
    {
        WeatherContext db;
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
            db.SaveChanges();
            return true;
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
            db.SaveChanges();
            return true;
        }



        // Get locations by country
        public List<Location> GetByCountry(string country)
        {
            return db.Locations
                .Where(l => l.Country.Equals(country, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Get locations by name (partial match)
        public List<Location> GetByName(string name)
        {
            return db.Locations
                .Where(l => l.Name.Contains(name))
                .ToList();
        }

        // Check if location exists
        public bool Exists(string name, string country)
        {
            return db.Locations.Any(l =>
                l.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                l.Country.Equals(country, StringComparison.OrdinalIgnoreCase));
        }

        // Get locations that have active alerts
        public List<Location> GetWithActiveAlerts()
        {
            return db.Locations
                     .Where(l => l.WeatherRecords.Any(wr => wr.LocationId == l.Id && wr.WeatherRecords.Any())) // Or join Alerts
                     .ToList();
        }
        public List<Location> GetNearby(decimal latitude, decimal longitude, double radiusKm)
        {
            const double EarthRadius = 6371; // km
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
