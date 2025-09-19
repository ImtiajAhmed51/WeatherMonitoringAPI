namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CraeteTableWeatherRecordAlert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Condition = c.String(nullable: false, maxLength: 50, unicode: false),
                        Message = c.String(nullable: false, maxLength: 50, unicode: false),
                        Severity = c.String(nullable: false, maxLength: 50, unicode: false),
                        TriggeredAt = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.WeatherRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordedAt = c.DateTime(nullable: false),
                        Temperature = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Humidity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WindSpeed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WindDirection = c.String(nullable: false, maxLength: 50, unicode: false),
                        Precipitation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeatherRecords", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Alerts", "LocationId", "dbo.Locations");
            DropIndex("dbo.WeatherRecords", new[] { "LocationId" });
            DropIndex("dbo.Alerts", new[] { "LocationId" });
            DropTable("dbo.WeatherRecords");
            DropTable("dbo.Alerts");
        }
    }
}
