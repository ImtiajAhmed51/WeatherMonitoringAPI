using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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


        public List<Location> GetByCountry(string country)
        {
            return db.Locations
                     .Where(l => l.Country.ToLower() == country.ToLower())
                     .ToList();
        }



        public List<Location> GetByName(string name)
        {
            return db.Locations
                     .Where(l => l.Name.ToLower().Contains(name.ToLower()))
                     .ToList();
        }

        public bool Exists(string name, string country)
        {
            return db.Locations.Any(l => l.Name == name && l.Country == country);
        }


        public List<Location> GetWithActiveAlerts()
        {
            return db.Locations
                     .Where(l => l.Alerts.Any(a => a.IsActive))
                     .ToList();
        }
        public List<Location> GetNearby(decimal latitude, decimal longitude, double radiusKm)
        {
            // earth radius in km
            const double EarthRadius = 6378; 


            double lat = (double)latitude;
            double lon = (double)longitude;

            double latRadius = radiusKm / EarthRadius * (180 / Math.PI);
            double lonRadius = radiusKm / (EarthRadius * Math.Cos(lat * Math.PI / 180)) * (180 / Math.PI);

            double minLat = lat - latRadius;
            double maxLat = lat + latRadius;
            double minLon = lon - lonRadius;
            double maxLon = lon + lonRadius;
            var candidates = db.Locations
                .Where(l => l.Latitude >= (decimal)minLat && l.Latitude <= (decimal)maxLat &&
                            l.Longitude >= (decimal)minLon && l.Longitude <= (decimal)maxLon)
                .ToList();

            return candidates.Where(l =>
            {
                var lat2 = (double)l.Latitude;
                var lon2 = (double)l.Longitude;
                var dLat = ToRadians(lat2 - lat);
                var dLon = ToRadians(lon2 - lon);
                var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                        Math.Cos(ToRadians(lat)) * Math.Cos(ToRadians(lat2)) *
                        Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                var distance = EarthRadius * c;
                return distance <= radiusKm;
            }).ToList();
        }
        public Location GetNearest(decimal latitude, decimal longitude, double radiusKm)
        {
            const double EarthRadius = 6378;

            double lat = (double)latitude;
            double lon = (double)longitude;

            double latRadius = radiusKm / EarthRadius * (180 / Math.PI);
            double lonRadius = radiusKm / (EarthRadius * Math.Cos(lat * Math.PI / 180)) * (180 / Math.PI);

            double minLat = lat - latRadius;
            double maxLat = lat + latRadius;
            double minLon = lon - lonRadius;
            double maxLon = lon + lonRadius;

            var candidates = db.Locations
                .Where(l => l.Latitude >= (decimal)minLat && l.Latitude <= (decimal)maxLat &&
                            l.Longitude >= (decimal)minLon && l.Longitude <= (decimal)maxLon)
                .ToList();

            Location nearest = null;
            double minDistance = double.MaxValue;

            foreach (var l in candidates)
            {
                double lat2 = (double)l.Latitude;
                double lon2 = (double)l.Longitude;
                double dLat = ToRadians(lat2 - lat);
                double dLon = ToRadians(lon2 - lon);
                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                           Math.Cos(ToRadians(lat)) * Math.Cos(ToRadians(lat2)) *
                           Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double distance = EarthRadius * c;

                if (distance <= radiusKm && distance < minDistance)
                {
                    minDistance = distance;
                    nearest = l;
                }
            }

            return nearest; 
        }

   

        private double ToRadians(double deg)
        {
            return deg * Math.PI / 180;
        }

    }
}
