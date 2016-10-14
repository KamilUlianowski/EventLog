namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Advertisements", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Advertisements", "Date", c => c.DateTime(nullable: false));
        }
    }
}
