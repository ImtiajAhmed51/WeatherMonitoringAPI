using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class WeatherStatsDTO
    {
        public int LocationId { get; set; }
        public int TotalRecords { get; set; }
        public double AverageTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double AverageHumidity { get; set; }
        public double MinHumidity { get; set; }
        public double MaxHumidity { get; set; }
        public double AveragePrecipitation { get; set; }
        public double TotalPrecipitation { get; set; }
        public double AverageWindSpeed { get; set; }
        public double MaxWindSpeed { get; set; }
        public DateTime FirstRecordDate { get; set; }
        public DateTime LastRecordDate { get; set; }

    }
}
