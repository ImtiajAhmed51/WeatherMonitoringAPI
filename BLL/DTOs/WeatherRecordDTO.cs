using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class WeatherRecordDTO
    {
        public int Id { get; set; }
        public DateTime RecordedAt { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public decimal Precipitation { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LocationId { get; set; }
    }
}
