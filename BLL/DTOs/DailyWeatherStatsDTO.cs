using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class DailyWeatherStatsDTO
    {
        public DateTime Date { get; set; }

        public double AverageTemperature { get; set; }

        public double MinTemperature { get; set; }

        public double MaxTemperature { get; set; }

        public double AverageHumidity { get; set; }

        public double TotalPrecipitation { get; set; }
        public int TotalRecords { get; set; }
    }
}
