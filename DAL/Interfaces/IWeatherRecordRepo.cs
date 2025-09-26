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
        List<WeatherRecord> GetByLocation(int locationId);
        List<WeatherRecord> GetByDateRange(DateTime start, DateTime end);
        List<WeatherRecord> GetLatestRecords();
        List<WeatherRecord> GetByTemperature(decimal min, decimal max);

        List<WeatherRecord> GetByHumidity(decimal min, decimal max);


    }
}
