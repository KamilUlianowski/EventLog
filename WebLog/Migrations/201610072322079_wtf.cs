namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "SendDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Messages", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Messages", "SendDate");
        }
    }
}
