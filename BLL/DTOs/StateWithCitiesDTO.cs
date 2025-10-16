using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class StateWithCitiesDTO:StateDTO
    {
        public List<CityDTO> Cities { get; set; }
    }
}
