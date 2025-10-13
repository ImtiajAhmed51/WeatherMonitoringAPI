namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Latitude = c.Decimal(precision: 9, scale: 6),
                        Longitude = c.Decimal(precision: 9, scale: 6),
                        PostalCode = c.String(maxLength: 20, unicode: false),
                        Population = c.Int(),
                        AreaSize = c.Decimal(precision: 18, scale: 2),
                        AreaType = c.String(maxLength: 50, unicode: false),
                        CityId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Latitude = c.Decimal(nullable: false, precision: 9, scale: 6),
                        Longitude = c.Decimal(nullable: false, precision: 9, scale: 6),
                        PostalCode = c.String(maxLength: 20, unicode: false),
                        Population = c.Int(),
                        Area = c.Decimal(precision: 18, scale: 2),
                        Elevation = c.Int(),
                        TimeZone = c.String(maxLength: 50, unicode: false),
                        CityType = c.String(maxLength: 50, unicode: false),
                        IsCapital = c.Boolean(nullable: false),
                        StateId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        StateCode = c.String(maxLength: 10, unicode: false),
                        Capital = c.String(maxLength: 100, unicode: false),
                        Population = c.Int(),
                        Area = c.Decimal(precision: 18, scale: 2),
                        TimeZone = c.String(maxLength: 50, unicode: false),
                        CountryId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        CountryCode = c.String(maxLength: 3, unicode: false),
                        Capital = c.String(maxLength: 100, unicode: false),
                        Currency = c.String(maxLength: 10, unicode: false),
                        PhoneCode = c.String(maxLength: 20, unicode: false),
                        Population = c.Int(),
                        Area = c.Decimal(precision: 18, scale: 2),
                        TimeZone = c.String(maxLength: 50, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Alerts", "Area_Id", c => c.Int());
            AddColumn("dbo.Alerts", "City_Id", c => c.Int());
            AddColumn("dbo.WeatherRecords", "City_Id", c => c.Int());
            AddColumn("dbo.WeatherRecords", "Area_Id", c => c.Int());
            CreateIndex("dbo.Alerts", "Area_Id");
            CreateIndex("dbo.Alerts", "City_Id");
            CreateIndex("dbo.WeatherRecords", "City_Id");
            CreateIndex("dbo.WeatherRecords", "Area_Id");
            AddForeignKey("dbo.Alerts", "Area_Id", "dbo.Areas", "Id");
            AddForeignKey("dbo.Alerts", "City_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.WeatherRecords", "City_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.WeatherRecords", "Area_Id", "dbo.Areas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeatherRecords", "Area_Id", "dbo.Areas");
            DropForeignKey("dbo.Areas", "CityId", "dbo.Cities");
            DropForeignKey("dbo.WeatherRecords", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropForeignKey("dbo.States", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Alerts", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Alerts", "Area_Id", "dbo.Areas");
            DropIndex("dbo.States", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropIndex("dbo.Areas", new[] { "CityId" });
            DropIndex("dbo.WeatherRecords", new[] { "Area_Id" });
            DropIndex("dbo.WeatherRecords", new[] { "City_Id" });
            DropIndex("dbo.Alerts", new[] { "City_Id" });
            DropIndex("dbo.Alerts", new[] { "Area_Id" });
            DropColumn("dbo.WeatherRecords", "Area_Id");
            DropColumn("dbo.WeatherRecords", "City_Id");
            DropColumn("dbo.Alerts", "City_Id");
            DropColumn("dbo.Alerts", "Area_Id");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Areas");
        }
    }
}
