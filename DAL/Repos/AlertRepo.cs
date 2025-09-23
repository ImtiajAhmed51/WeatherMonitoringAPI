using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class AlertRepo : IRepo<Alert, int, bool>
    {
        WeatherContext db;
        public AlertRepo()
        {
            db = WeatherContextSingleton.GetInstance();
        }
        public bool Create(Alert obj)
        {
           db.Alerts.Add(obj);
           return db.SaveChanges()>0;
        }

        public bool Delete(int id)
        {
            var alert = Get(id);
            if (alert == null) 
                return false;

            db.Alerts.Remove(alert);
            db.SaveChanges();
            return true;
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
            var var = Get(obj.Id);
            if (var == null)
                return false;

            db.Entry(var).CurrentValues.SetValues(obj);
            db.SaveChanges();
            return true;
        }
    }
}
