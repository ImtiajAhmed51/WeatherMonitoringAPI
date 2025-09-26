using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class LocationWithWeatherRecordAndAlertDTO:LocationDTO

    {
        public List<WeatherRecordDTO> WeatherRecords { get; set; } 
        public List<AlertDTO> Alerts { get; set; }
    }
}
