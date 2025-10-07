using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class AlertRepo : IRepo<Alert, int, bool>, IAlertRepo
    {
        WeatherContext db;
        public AlertRepo()
        {
            db = WeatherContextSingleton.GetInstance();
        }

        public bool Create(Alert obj)
        {
            db.Alerts.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var alert = Get(id);
            if (alert == null)
                return false;
            db.Alerts.Remove(alert);
            return db.SaveChanges() > 0;
        }

        public List<Alert> Get()
        {
            return db.Alerts.ToList();
        }

        public Alert Get(int id)
        {
            return db.Alerts.Find(id);
        }

        public bool Update(Alert obj)
        {
            var existingAlert = Get(obj.Id);
            if (existingAlert == null)
                return false;
            db.Entry(existingAlert).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }

        public Alert GetAlertWithLocation(int id)
        {
            return db.Alerts.Include(a => a.Location)
                            .FirstOrDefault(a => a.Id == id);
        }

        public List<Alert> GetAllWithLocations()
        {
            return db.Alerts.Include(a => a.Location).ToList();
        }

        public List<Alert> GetByLocation(int locationId)
        {
            return db.Alerts.Where(a => a.LocationId == locationId)
                           .OrderByDescending(a => a.CreatedAt)
                           .ToList();
        }

        public List<Alert> GetActiveAlerts()
        {
            return db.Alerts.Where(a => a.IsActive).ToList();
        }

        public int GetAlertCountByLocation(int locationId)
        {
            return db.Alerts.Count(a => a.LocationId == locationId);
        }
    }
}
