using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Country:BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(3)]
        public string CountryCode { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Capital { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(10)]
        public string Currency { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        public string PhoneCode { get; set; }

        public int? Population { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? Area { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string TimeZone { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
