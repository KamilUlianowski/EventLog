namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asfasdfsa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolGrades", "MyTest_Id", c => c.Int());
            CreateIndex("dbo.SchoolGrades", "MyTest_Id");
            AddForeignKey("dbo.SchoolGrades", "MyTest_Id", "dbo.Tests", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolGrades", "MyTest_Id", "dbo.Tests");
            DropIndex("dbo.SchoolGrades", new[] { "MyTest_Id" });
            DropColumn("dbo.SchoolGrades", "MyTest_Id");
        }
    }
}
