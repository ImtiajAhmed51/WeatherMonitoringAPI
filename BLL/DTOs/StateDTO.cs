using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class StateDTO
    {
        [Required(ErrorMessage = "State name is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public string StateCode { get; set; }
        public string Capital { get; set; }
        public int? Population { get; set; }
        public decimal? Area { get; set; }
        public string TimeZone { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
