namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subjectAndTeachers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        Teacher_Id = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_Id, t.Subject_Id })
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Subject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherSubjects", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSubjects", "Teacher_Id", "dbo.Teacher");
            DropIndex("dbo.TeacherSubjects", new[] { "Subject_Id" });
            DropIndex("dbo.TeacherSubjects", new[] { "Teacher_Id" });
            DropTable("dbo.TeacherSubjects");
        }
    }
}
