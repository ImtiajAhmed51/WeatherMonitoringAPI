namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetablemessage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Alerts", "Message", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alerts", "Message", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
