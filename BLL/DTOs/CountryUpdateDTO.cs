using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CountryUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(3, MinimumLength = 2)]
        public string CountryCode { get; set; }

        public string Capital { get; set; }
        public string Currency { get; set; }
        public string PhoneCode { get; set; }
        public int? Population { get; set; }
        public decimal? Area { get; set; }
        public string TimeZone { get; set; }
        public bool IsActive { get; set; }
    }
}
