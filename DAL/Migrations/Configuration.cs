namespace DAL.Migrations
{
    using DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Models.WeatherContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Models.WeatherContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.




            // random data entry......


            //string[] sampleCities = { "Dhaka", "Chittagong", "Sylhet", "New York", "London", "Toronto" };
            //string[] sampleCountries = { "Bangladesh", "Bangladesh", "Bangladesh", "USA", "UK", "Canada" };
            //var random = new Random();

            //for (int i = 0; i < sampleCities.Length; i++)
            //{
            //    context.Locations.Add(new Location { 

            //        Name=sampleCities[i],
            //        Latitude = Math.Round((decimal)(random.NextDouble() * 180 - 90), 6),
            //        Longitude = Math.Round((decimal)(random.NextDouble() * 360 - 180), 6),
            //        Country= sampleCountries[i]
            //    });
            //}
            //context.SaveChanges();


           // var locations = context.Locations.ToList();
            //var random = new Random();

            //string[] windDirections = { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };

            //foreach (var loc in locations)
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        var record = new WeatherRecord
            //        {
            //            RecordedAt = DateTime.UtcNow.AddHours(-random.Next(0, 72)), 
            //            Temperature = Math.Round((decimal)(random.NextDouble() * 40 - 10), 2), 
            //            Humidity = Math.Round((decimal)(random.NextDouble() * 100), 2), 
            //            WindSpeed = Math.Round((decimal)(random.NextDouble() * 15), 2), 
            //            WindDirection = windDirections[random.Next(windDirections.Length)],
            //            Precipitation = Math.Round((decimal)(random.NextDouble() * 20), 2), 
            //            CreatedAt = DateTime.UtcNow,
            //            LocationId = loc.Id
            //        };

            //        context.WeatherRecords.AddOrUpdate(
            //            w => new { w.LocationId, w.RecordedAt },
            //            record
            //        );
            //    }
            //}
            //context.SaveChanges();



            //string[] conditions = { "High Temperature", "Heavy Rainfall", "Storm Warning", "Low Humidity", "Strong Winds" };
            //string[] severities = { "Low", "Medium", "High", "Critical" };
            //var random = new Random();
            //foreach (var loc in locations)
            //{
            //    for (int i = 0; i < 3; i++) // 3 alerts per location
            //    {
            //        string condition = conditions[random.Next(conditions.Length)];
            //        string severity = severities[random.Next(severities.Length)];

            //        var alert = new Alert
            //        {
            //            Condition = condition,
            //            Message = $"{condition} detected at {loc.Name}. Please take necessary precautions.",
            //            Severity = severity,
            //            TriggeredAt = DateTime.UtcNow.AddHours(-random.Next(0, 48)),
            //            IsActive = random.Next(0, 2) == 1, // randomly active or not
            //            LocationId = loc.Id
            //        };

            //        context.Alerts.AddOrUpdate(
            //            a => new { a.LocationId, a.Condition, a.TriggeredAt },
            //            alert
            //        );
            //    }
            //}
            //context.SaveChanges();
        }
    }
}
