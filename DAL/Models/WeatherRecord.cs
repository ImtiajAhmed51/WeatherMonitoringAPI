using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class WeatherRecord : BaseEntity
    {
        [Required]
        public DateTime RecordedAt { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Temperature { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Humidity { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal WindSpeed { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string WindDirection { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Precipitation { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string WeatherCondition { get; set; } 

        [Column(TypeName = "decimal")]
        public decimal? Pressure { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? Visibility { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? UVIndex { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }
        public virtual City City { get; set; }

        [ForeignKey("Area")]
        public int? AreaId { get; set; }
        public virtual Area Area { get; set; }
    }
}