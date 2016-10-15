namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Birth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Birth", c => c.DateTime(nullable: false));
        }
    }
}
