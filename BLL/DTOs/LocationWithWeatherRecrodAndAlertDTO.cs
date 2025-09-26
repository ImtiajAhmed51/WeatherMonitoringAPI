using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class LocationWithWeatherRecrodAndAlertDTO:LocationDTO

    {
        public List<WeatherRecordDTO> WeatherRecords { get; set; } 
        public List<AlertDTO> Alerts { get; set; }
    }
}
