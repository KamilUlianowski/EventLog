namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "SendDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.SchoolGrades", "Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SchoolGrades", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Messages", "SendDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
