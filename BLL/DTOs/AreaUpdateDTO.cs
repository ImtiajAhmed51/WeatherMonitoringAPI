using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AreaUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(-90, 90)]
        public decimal? Latitude { get; set; }

        [Range(-180, 180)]
        public decimal? Longitude { get; set; }

        public string PostalCode { get; set; }
        public int? Population { get; set; }
        public decimal? AreaSize { get; set; }
        public string AreaType { get; set; }
        public int CityId { get; set; }
        public bool IsActive { get; set; }
    }
}
