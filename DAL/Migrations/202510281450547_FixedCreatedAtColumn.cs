namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedCreatedAtColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Areas", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cities", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.States", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Countries", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Countries", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.States", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cities", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Areas", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
