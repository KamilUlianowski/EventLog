namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGrades2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchoolGrades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Int(nullable: false),
                        Subject_Id = c.Int(),
                        Teacher_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Subject_Id)
                .Index(t => t.Teacher_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolGrades", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SchoolGrades", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.SchoolGrades", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.SchoolGrades", new[] { "User_Id" });
            DropIndex("dbo.SchoolGrades", new[] { "Teacher_Id" });
            DropIndex("dbo.SchoolGrades", new[] { "Subject_Id" });
            DropTable("dbo.SchoolGrades");
        }
    }
}
