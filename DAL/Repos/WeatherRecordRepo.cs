using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class WeatherRecordRepo : IRepo<WeatherRecord, int, bool>, IWeatherRecordRepo
    {
        private WeatherContext db;
        public WeatherRecordRepo()
        {
            db = WeatherContextSingleton.GetInstance();
        }
        public bool Create(WeatherRecord obj)
        {
            db.WeatherRecords.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var weatherRecord = Get(id);
            if (weatherRecord == null)
                return false;

            db.WeatherRecords.Remove(weatherRecord);

            return db.SaveChanges() > 0;
        }

        public List<WeatherRecord> Get()
        {
            return db.WeatherRecords.ToList();
        }

        public WeatherRecord Get(int id)
        {
            return db.WeatherRecords.Find(id);
        }

        public bool Update(WeatherRecord obj)
        {

            var var = Get(obj.Id);
            if (var == null)
                return false;

            db.Entry(var).CurrentValues.SetValues(obj);

            return db.SaveChanges() > 0;
        }

        public List<WeatherRecord> GetByLocation(int locationId)
        {
            return db.WeatherRecords
                     .Where(w => w.LocationId == locationId)
                     .Include(w => w.Location)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }



        public List<WeatherRecord> GetByDateRange(DateTime start, DateTime end)
        {
            return db.WeatherRecords
                     .Where(w => w.RecordedAt >= start && w.RecordedAt <= end)
                     .Include(w => w.Location)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }

        public List<WeatherRecord> GetLatestRecords()
        {
            var latest = db.WeatherRecords
                                 .GroupBy(w => w.LocationId)
                                 .Select(g => g.OrderByDescending(w => w.RecordedAt).FirstOrDefault())
                                 .Include(w => w.Location)
                                 .ToList();

            return latest;
        }

        public List<WeatherRecord> GetByTemperature(decimal min, decimal max)
        {
            return db.WeatherRecords
                     .Where(w => w.Temperature >= min && w.Temperature <= max)
                     .Include(w => w.Location)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }
        public List<WeatherRecord> GetByHumidity(decimal min, decimal max)
        {
            return db.WeatherRecords
                     .Where(w => w.Humidity >= min && w.Humidity <= max)
                     .Include(w => w.Location)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }


      
    }
}
