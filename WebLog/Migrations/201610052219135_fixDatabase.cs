namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SchoolGrades", "User_Id", "dbo.Users");
            DropIndex("dbo.SchoolGrades", new[] { "User_Id" });
            AddColumn("dbo.SchoolGrades", "Student_Id", c => c.Int());
            CreateIndex("dbo.SchoolGrades", "Student_Id");
            AddForeignKey("dbo.SchoolGrades", "Student_Id", "dbo.Parent", "Id");
            DropColumn("dbo.SchoolGrades", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchoolGrades", "User_Id", c => c.Int());
            DropForeignKey("dbo.SchoolGrades", "Student_Id", "dbo.Parent");
            DropIndex("dbo.SchoolGrades", new[] { "Student_Id" });
            DropColumn("dbo.SchoolGrades", "Student_Id");
            CreateIndex("dbo.SchoolGrades", "User_Id");
            AddForeignKey("dbo.SchoolGrades", "User_Id", "dbo.Users", "Id");
        }
    }
}
