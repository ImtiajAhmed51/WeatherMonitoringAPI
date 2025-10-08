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

            var data = Get(obj.Id);
            if (data == null)
                return false;

            db.Entry(data).CurrentValues.SetValues(obj);

            return db.SaveChanges() > 0;
        }

        public WeatherRecord GetWithLocation(int id)
        {
            return db.WeatherRecords
                     .Include(w => w.Location)
                     .FirstOrDefault(w => w.Id == id);
        }

        public List<WeatherRecord> GetAllWithLocations()
        {
            return db.WeatherRecords
                     .Include(w => w.Location)
                     .ToList();
        }

        public List<WeatherRecord> GetByLocation(int locationId)
        {
            return db.WeatherRecords
                     .Where(w => w.LocationId == locationId)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }

        public List<WeatherRecord> GetByLocationWithLocation(int locationId)
        {
            return db.WeatherRecords
                     .Where(w => w.LocationId == locationId)
                     .Include(w => w.Location)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }

        public WeatherRecord GetLatestByLocation(int locationId)
        {
            return db.WeatherRecords
                     .Where(w => w.LocationId == locationId)
                     .OrderByDescending(w => w.RecordedAt)
                     .FirstOrDefault();
        }

        public List<WeatherRecord> GetByDateRange(DateTime start, DateTime end)
        {
            return db.WeatherRecords
                     .Where(w => w.RecordedAt >= start && w.RecordedAt <= end)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }

        public List<WeatherRecord> GetByLocationAndDateRange(int locationId, DateTime start, DateTime end)
        {
            return db.WeatherRecords
                     .Where(w => w.LocationId == locationId &&
                                 w.RecordedAt >= start &&
                                 w.RecordedAt <= end)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }

        public List<WeatherRecord> GetRecordedAfter(DateTime date)
        {
            return db.WeatherRecords
                     .Where(w => w.RecordedAt > date)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }

        public List<WeatherRecord> GetRecordedBefore(DateTime date)
        {
            return db.WeatherRecords
                     .Where(w => w.RecordedAt < date)
                     .OrderByDescending(w => w.RecordedAt)
                     .ToList();
        }

        public int GetRecordCountByLocation(int locationId)
        {
            return db.WeatherRecords.Count(w => w.LocationId == locationId);
        }

        public int GetTotalRecordCount()
        {
            return db.WeatherRecords.Count();
        }

        public bool Exists(int id)
        {
            return db.WeatherRecords.Any(w => w.Id == id);
        }

        public bool RecordExistsForLocation(int locationId, DateTime recordedAt)
        {
            return db.WeatherRecords.Any(w => w.LocationId == locationId &&
                                              w.RecordedAt == recordedAt);
        }

        public List<WeatherRecord> GetRecentRecords(int count)
        {
            return db.WeatherRecords
                     .OrderByDescending(w => w.RecordedAt)
                     .Take(count)
                     .ToList();
        }

        public List<WeatherRecord> GetRecentRecordsByLocation(int locationId, int count)
        {
            return db.WeatherRecords
                     .Where(w => w.LocationId == locationId)
                     .OrderByDescending(w => w.RecordedAt)
                     .Take(count)
                     .ToList();
        }

        public DateTime? GetFirstRecordDate(int locationId)
        {
            var firstRecord = db.WeatherRecords
                               .Where(w => w.LocationId == locationId)
                               .OrderBy(w => w.RecordedAt)
                               .FirstOrDefault();
            return firstRecord?.RecordedAt;
        }

        public DateTime? GetLastRecordDate(int locationId)
        {
            var lastRecord = db.WeatherRecords
                              .Where(w => w.LocationId == locationId)
                              .OrderByDescending(w => w.RecordedAt)
                              .FirstOrDefault();
            return lastRecord?.RecordedAt;
        }



    }
}
