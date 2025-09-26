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
        public Alert GetAlertLocation(int id)
        {
            var alert= db.Alerts.Include(a => a.Location)
                            .FirstOrDefault(a => a.Id == id);
            return alert;
        }
        public List<Alert> GetAlertsLocation()
        {
            return db.Alerts.Include(a => a.Location).ToList();
        }
        public bool Update(Alert obj)
        {
            var var = Get(obj.Id);
            if (var == null)
                return false;

            db.Entry(var).CurrentValues.SetValues(obj);

            return db.SaveChanges() > 0;
        }

        public List<Alert> GetByLocation(int locationId, bool onlyActive = true)
        {
            var query = db.Alerts.Where(a => a.LocationId == locationId);
            if (onlyActive)
                query = query.Where(a => a.IsActive && (a.ExpiresAt == null || a.ExpiresAt > DateTime.UtcNow));

            return query.OrderByDescending(a => a.CreatedAt).ToList();
        }
        public int DeactivateAll()
        {
            var activeAlerts = db.Alerts.Where(a => a.IsActive).ToList();
            foreach (var alert in activeAlerts)
            {
                alert.IsActive = false;
            }
            return db.SaveChanges();
        }

        public List<Alert> GetExpiringSoon(double hours = 6)
        {
            var cutoff = DateTime.UtcNow.AddHours(hours);
            return db.Alerts
                .Where(a => a.IsActive && a.ExpiresAt != null && a.ExpiresAt <= cutoff)
                .OrderBy(a => a.ExpiresAt)
                .ToList();
        }
    }
}
