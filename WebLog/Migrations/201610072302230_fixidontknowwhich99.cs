namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixidontknowwhich99 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "SendDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
