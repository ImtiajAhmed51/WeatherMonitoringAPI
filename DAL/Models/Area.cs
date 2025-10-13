using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Area:BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? Longitude { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string PostalCode { get; set; }

        public int? Population { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? AreaSize { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string AreaType { get; set; }

        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; } 
        public virtual ICollection<WeatherRecord> WeatherRecords { get; set; }
    }
}
