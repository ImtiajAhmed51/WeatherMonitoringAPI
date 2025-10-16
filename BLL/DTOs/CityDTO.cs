using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CityDTO:BaseDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(-90, 90)]
        public decimal Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public decimal Longitude { get; set; }

        public string PostalCode { get; set; }
        public int? Population { get; set; }
        public decimal? Area { get; set; }
        public int? Elevation { get; set; }
        public string TimeZone { get; set; }
        public string CityType { get; set; }
        public bool IsCapital { get; set; }

        [Required]
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
    }
}
