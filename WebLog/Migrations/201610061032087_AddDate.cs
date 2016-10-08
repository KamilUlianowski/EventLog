namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolGrades", "SendDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SchoolGrades", "SendDate");
        }
    }
}
