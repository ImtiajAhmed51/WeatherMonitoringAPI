using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class LocationWithAlertsDTO: LocationDTO
    {
        public List<AlertDTO> Alerts { get; set; }
    }
}
