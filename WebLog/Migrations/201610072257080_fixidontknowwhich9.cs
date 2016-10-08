namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixidontknowwhich9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "SendDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "SendDate", c => c.DateTime(nullable: false));
        }
    }
}
