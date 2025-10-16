using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CountryCreateDTO
    {
        [Required(ErrorMessage = "Country name is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country code is required")]
        [StringLength(3, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{2,3}$", ErrorMessage = "Country code must be 2-3 uppercase letters")]
        public string CountryCode { get; set; }

        public string Capital { get; set; }
        public string Currency { get; set; }
        public string PhoneCode { get; set; }
        public int? Population { get; set; }
        public decimal? Area { get; set; }
        public string TimeZone { get; set; }
    }
}
