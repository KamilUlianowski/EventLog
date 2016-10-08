namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bazakz : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        SendDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UserFrom_Id = c.Int(),
                        UserTo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserFrom_Id)
                .ForeignKey("dbo.Users", t => t.UserTo_Id)
                .Index(t => t.UserFrom_Id)
                .Index(t => t.UserTo_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Birth = c.DateTime(nullable: false),
                        Email = c.String(),
                        Pesel = c.String(),
                        Adress = c.String(),
                        Phone = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchoolClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchoolGrades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Int(nullable: false),
                        Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Student_Id = c.Int(),
                        Subject_Id = c.Int(),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Student", t => t.Student_Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Subject_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.SubjectSchoolClasses",
                c => new
                    {
                        Subject_Id = c.Int(nullable: false),
                        SchoolClass_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subject_Id, t.SchoolClass_Id })
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClass_Id, cascadeDelete: true)
                .Index(t => t.Subject_Id)
                .Index(t => t.SchoolClass_Id);
            
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
            
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Student", t => t.Student_Id)
                .Index(t => t.Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SchoolClass_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClass_Id)
                .Index(t => t.Id)
                .Index(t => t.SchoolClass_Id);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teacher", "Id", "dbo.Users");
            DropForeignKey("dbo.Student", "SchoolClass_Id", "dbo.SchoolClasses");
            DropForeignKey("dbo.Student", "Id", "dbo.Users");
            DropForeignKey("dbo.Parent", "Student_Id", "dbo.Student");
            DropForeignKey("dbo.Parent", "Id", "dbo.Users");
            DropForeignKey("dbo.SchoolGrades", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.SchoolGrades", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SchoolGrades", "Student_Id", "dbo.Student");
            DropForeignKey("dbo.Messages", "UserTo_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserFrom_Id", "dbo.Users");
            DropForeignKey("dbo.SchoolClasses", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.TeacherSubjects", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSubjects", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.SubjectSchoolClasses", "SchoolClass_Id", "dbo.SchoolClasses");
            DropForeignKey("dbo.SubjectSchoolClasses", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Teacher", new[] { "Id" });
            DropIndex("dbo.Student", new[] { "SchoolClass_Id" });
            DropIndex("dbo.Student", new[] { "Id" });
            DropIndex("dbo.Parent", new[] { "Student_Id" });
            DropIndex("dbo.Parent", new[] { "Id" });
            DropIndex("dbo.TeacherSubjects", new[] { "Subject_Id" });
            DropIndex("dbo.TeacherSubjects", new[] { "Teacher_Id" });
            DropIndex("dbo.SubjectSchoolClasses", new[] { "SchoolClass_Id" });
            DropIndex("dbo.SubjectSchoolClasses", new[] { "Subject_Id" });
            DropIndex("dbo.SchoolGrades", new[] { "Teacher_Id" });
            DropIndex("dbo.SchoolGrades", new[] { "Subject_Id" });
            DropIndex("dbo.SchoolGrades", new[] { "Student_Id" });
            DropIndex("dbo.SchoolClasses", new[] { "Teacher_Id" });
            DropIndex("dbo.Messages", new[] { "UserTo_Id" });
            DropIndex("dbo.Messages", new[] { "UserFrom_Id" });
            DropTable("dbo.Teacher");
            DropTable("dbo.Student");
            DropTable("dbo.Parent");
            DropTable("dbo.TeacherSubjects");
            DropTable("dbo.SubjectSchoolClasses");
            DropTable("dbo.SchoolGrades");
            DropTable("dbo.Subjects");
            DropTable("dbo.SchoolClasses");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
