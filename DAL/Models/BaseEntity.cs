using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
