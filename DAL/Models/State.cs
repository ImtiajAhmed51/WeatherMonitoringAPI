using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class State:BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string StateCode { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Capital { get; set; }

        public int? Population { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Area { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string TimeZone { get; set; }

        [Required]
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
