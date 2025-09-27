namespace DAL.Migrations
{
    using DAL.Models;
    using System;
    using System.Collections.Generic;
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


            //var locations = new List<Location> {
            //    new Location { Name = "Dhaka",      Country = "Bangladesh", Latitude = 23.8103m, Longitude = 90.4125m },
            //    new Location { Name = "Chattogram", Country = "Bangladesh", Latitude = 22.3569m, Longitude = 91.7832m },
            //    new Location { Name = "Khulna",     Country = "Bangladesh", Latitude = 22.8456m, Longitude = 89.5403m },
            //    new Location { Name = "Rajshahi",   Country = "Bangladesh", Latitude = 24.3745m, Longitude = 88.6042m },
            //    new Location { Name = "Barishal",   Country = "Bangladesh", Latitude = 22.7010m, Longitude = 90.3535m },
            //    new Location { Name = "Sylhet",     Country = "Bangladesh", Latitude = 24.8949m, Longitude = 91.8687m },
            //    new Location { Name = "Rangpur",    Country = "Bangladesh", Latitude = 25.7439m, Longitude = 89.2752m },
            //    new Location { Name = "Mymensingh", Country = "Bangladesh", Latitude = 24.7471m, Longitude = 90.4203m }
            // };

            //foreach (var location in locations) { 

            //    context.Locations.Add(location);

            //}
            //context.SaveChanges();


            //var locations = context.Locations.ToList();
            //var rnd = new Random();
            //var weatherRecords = new List<WeatherRecord>();

            //foreach (var location in locations)
            //{
            //    for (int dayOffset = 0; dayOffset < 7; dayOffset++) 
            //    {
            //        var currentDate = DateTime.Today.AddDays(-dayOffset);

            //        for (int recordPerDay = 0; recordPerDay < 3; recordPerDay++) 
            //        {
            //            var recordedAt = currentDate.AddHours(rnd.Next(0, 24))
            //                                        .AddMinutes(rnd.Next(0, 60))
            //                                        .AddSeconds(rnd.Next(0, 60));

            //            var record = new WeatherRecord
            //            {
            //                LocationId = location.Id,
            //                Location = location,
            //                RecordedAt = recordedAt,
            //                Temperature = (decimal)(rnd.NextDouble() * 15 + 15), 
            //                Humidity = (decimal)(rnd.NextDouble() * 50 + 30),    
            //                WindSpeed = (decimal)(rnd.NextDouble() * 10),        
            //                WindDirection = GetRandomWindDirection(rnd),
            //                Precipitation = (decimal)(rnd.NextDouble() * 20),   
            //                CreatedAt = DateTime.Now
            //            };

            //            weatherRecords.Add(record);
            //        }
            //    }
            //}

            //context.WeatherRecords.AddRange(weatherRecords);
            //context.SaveChanges();



            //var rnd = new Random();
            //var alertConditions = new string[] { "Storm", "Heavy Rain", "High Wind", "Flood", "Heatwave" };
            //var alertSeverities = new string[] { "Low", "Moderate", "High", "Critical" };
            //var alerts = new List<Alert>();

            //foreach (var location in locations)
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        var triggeredAt = DateTime.Now.AddHours(-rnd.Next(0, 72)); 
            //        var expiresAt = triggeredAt.AddHours(rnd.Next(1, 24));     

            //        var alert = new Alert
            //        {
            //            LocationId = location.Id,
            //            Location = location,
            //            Condition = alertConditions[rnd.Next(alertConditions.Length)],
            //            Message = $"Alert for {location.Name}: {alertConditions[rnd.Next(alertConditions.Length)]} expected",
            //            Severity = alertSeverities[rnd.Next(alertSeverities.Length)],
            //            TriggeredAt = triggeredAt,
            //            ExpiresAt = expiresAt,
            //            CreatedAt = DateTime.Now,
            //            UpdatedAt = null,
            //            IsActive = rnd.NextDouble() > 0.3 // 70% chance active
            //        };

            //        alerts.Add(alert);
            //    }
            //}

            //context.Alerts.AddOrUpdate(a => new { a.LocationId, a.TriggeredAt }, alerts.ToArray());
            //context.SaveChanges();
        }
    }
}
