namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationIdToWeatherRecord : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.WeatherRecords", name: "Area_Id", newName: "AreaId");
            RenameColumn(table: "dbo.WeatherRecords", name: "City_Id", newName: "CityId");
            RenameIndex(table: "dbo.WeatherRecords", name: "IX_City_Id", newName: "IX_CityId");
            RenameIndex(table: "dbo.WeatherRecords", name: "IX_Area_Id", newName: "IX_AreaId");
            AddColumn("dbo.WeatherRecords", "WeatherCondition", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.WeatherRecords", "Pressure", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.WeatherRecords", "Visibility", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.WeatherRecords", "UVIndex", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.WeatherRecords", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.WeatherRecords", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.WeatherRecords", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.WeatherRecords", "CreatedBy", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.WeatherRecords", "UpdatedBy", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.WeatherRecords", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WeatherRecords", "RowVersion");
            DropColumn("dbo.WeatherRecords", "UpdatedBy");
            DropColumn("dbo.WeatherRecords", "CreatedBy");
            DropColumn("dbo.WeatherRecords", "UpdatedAt");
            DropColumn("dbo.WeatherRecords", "IsDeleted");
            DropColumn("dbo.WeatherRecords", "IsActive");
            DropColumn("dbo.WeatherRecords", "UVIndex");
            DropColumn("dbo.WeatherRecords", "Visibility");
            DropColumn("dbo.WeatherRecords", "Pressure");
            DropColumn("dbo.WeatherRecords", "WeatherCondition");
            RenameIndex(table: "dbo.WeatherRecords", name: "IX_AreaId", newName: "IX_Area_Id");
            RenameIndex(table: "dbo.WeatherRecords", name: "IX_CityId", newName: "IX_City_Id");
            RenameColumn(table: "dbo.WeatherRecords", name: "CityId", newName: "City_Id");
            RenameColumn(table: "dbo.WeatherRecords", name: "AreaId", newName: "Area_Id");
        }
    }
}
