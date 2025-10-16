using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CountryDTO:BaseDTO
    {
        [Required(ErrorMessage = "Country name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Country name must be between 2 and 100 characters")]
        public string Name { get; set; }

        [StringLength(3, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{2,3}$", ErrorMessage = "Country code must be 2-3 uppercase letters")]
        public string CountryCode { get; set; }

        [StringLength(100)]
        public string Capital { get; set; }

        [StringLength(10)]
        public string Currency { get; set; }

        [RegularExpression(@"^\+\d{1,4}$", ErrorMessage = "Phone code must start with + followed by 1-4 digits")]
        public string PhoneCode { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Population must be positive")]
        public int? Population { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Area must be positive")]
        public decimal? Area { get; set; }

        public string TimeZone { get; set; }

    }
}
