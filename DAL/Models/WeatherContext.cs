using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WeatherContext:DbContext
    {
     
        public DbSet <Location> Locations { get; set; }
        public DbSet<WeatherRecord> WeatherRecords { get; set; }
        public DbSet<Alert> Alerts { get; set; }
    }
}
