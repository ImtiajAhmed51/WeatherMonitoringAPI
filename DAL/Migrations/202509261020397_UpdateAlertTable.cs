namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAlertTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alerts", "ExpiresAt", c => c.DateTime());
            AddColumn("dbo.Alerts", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Alerts", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alerts", "UpdatedAt");
            DropColumn("dbo.Alerts", "CreatedAt");
            DropColumn("dbo.Alerts", "ExpiresAt");
        }
    }
}
