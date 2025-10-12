using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class City:BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,6)")]
        public decimal Latitude { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,6)")]
        public decimal Longitude { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string PostalCode { get; set; }

        public int? Population { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Area { get; set; }

        public int? Elevation { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string TimeZone { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string CityType { get; set; }

        public bool IsCapital { get; set; } = false;

        [Required]
        [ForeignKey("State")]
        public int StateId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; } 
        public virtual ICollection<WeatherRecord> WeatherRecords { get; set; } 
    }
}
