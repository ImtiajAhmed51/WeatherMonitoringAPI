using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class WeatherRecordWithLocationDTO: WeatherRecordDTO
    {
        public List<LocationDTO> Locations { get; set; }
    }
}
