using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherRecordRepo
    {
        WeatherRecord GetWithLocation(int id);

        List<WeatherRecord> GetAllWithLocations();

        List<WeatherRecord> GetByLocation(int locationId);

        List<WeatherRecord> GetByLocationWithLocation(int locationId);

        WeatherRecord GetLatestByLocation(int locationId);

        List<WeatherRecord> GetByDateRange(DateTime start, DateTime end);

        List<WeatherRecord> GetByLocationAndDateRange(int locationId, DateTime start, DateTime end);

        List<WeatherRecord> GetRecordedAfter(DateTime date);

        List<WeatherRecord> GetRecordedBefore(DateTime date);

        int GetRecordCountByLocation(int locationId);

        int GetTotalRecordCount();

        bool Exists(int id);

        bool RecordExistsForLocation(int locationId, DateTime recordedAt);
        List<WeatherRecord> GetRecentRecords(int count);
        List<WeatherRecord> GetRecentRecordsByLocation(int locationId, int count);

        DateTime? GetFirstRecordDate(int locationId);

        DateTime? GetLastRecordDate(int locationId);
    }
}
