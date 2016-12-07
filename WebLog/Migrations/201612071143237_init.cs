namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Surname = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 30),
                        Pesel = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 30),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Title = c.String(),
                        Visible = c.Boolean(nullable: false),
                        MainPage = c.Boolean(nullable: false),
                        OnlyForParents = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.SchoolClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.SchoolGrades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        MyTest_Id = c.Int(),
                        Student_Id = c.Int(),
                        Subject_Id = c.Int(),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.MyTest_Id)
                .ForeignKey("dbo.Student", t => t.Student_Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id)
                .Index(t => t.MyTest_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Subject_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Time = c.Int(nullable: false),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CorrectAnswer = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Test_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.Test_Id)
                .Index(t => t.Test_Id);
            
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Content = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.SubjectFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        SendDate = c.DateTime(nullable: false),
                        UserFrom_Id = c.Int(),
                        UserTo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserFrom_Id)
                .ForeignKey("dbo.Users", t => t.UserTo_Id)
                .Index(t => t.UserFrom_Id)
                .Index(t => t.UserTo_Id);
            
            CreateTable(
                "dbo.SchoolClassAdvertisements",
                c => new
                    {
                        SchoolClass_Id = c.Int(nullable: false),
                        Advertisement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchoolClass_Id, t.Advertisement_Id })
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClass_Id, cascadeDelete: true)
                .ForeignKey("dbo.Advertisements", t => t.Advertisement_Id, cascadeDelete: true)
                .Index(t => t.SchoolClass_Id)
                .Index(t => t.Advertisement_Id);
            
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
                "dbo.Admin",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Parent_Id = c.Int(),
                        SchoolClass_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Parent", t => t.Parent_Id)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClass_Id)
                .Index(t => t.Id)
                .Index(t => t.Parent_Id)
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
            DropForeignKey("dbo.Student", "Parent_Id", "dbo.Parent");
            DropForeignKey("dbo.Student", "Id", "dbo.Users");
            DropForeignKey("dbo.Parent", "Id", "dbo.Users");
            DropForeignKey("dbo.Admin", "Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserTo_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserFrom_Id", "dbo.Users");
            DropForeignKey("dbo.SubjectFiles", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SchoolGrades", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.SchoolGrades", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SchoolGrades", "Student_Id", "dbo.Student");
            DropForeignKey("dbo.SchoolGrades", "MyTest_Id", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSubjects", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSubjects", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.SchoolClasses", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.Advertisements", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.SubjectSchoolClasses", "SchoolClass_Id", "dbo.SchoolClasses");
            DropForeignKey("dbo.SubjectSchoolClasses", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Questions", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.QuestionAnswers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.SchoolClassAdvertisements", "Advertisement_Id", "dbo.Advertisements");
            DropForeignKey("dbo.SchoolClassAdvertisements", "SchoolClass_Id", "dbo.SchoolClasses");
            DropIndex("dbo.Teacher", new[] { "Id" });
            DropIndex("dbo.Student", new[] { "SchoolClass_Id" });
            DropIndex("dbo.Student", new[] { "Parent_Id" });
            DropIndex("dbo.Student", new[] { "Id" });
            DropIndex("dbo.Parent", new[] { "Id" });
            DropIndex("dbo.Admin", new[] { "Id" });
            DropIndex("dbo.TeacherSubjects", new[] { "Subject_Id" });
            DropIndex("dbo.TeacherSubjects", new[] { "Teacher_Id" });
            DropIndex("dbo.SubjectSchoolClasses", new[] { "SchoolClass_Id" });
            DropIndex("dbo.SubjectSchoolClasses", new[] { "Subject_Id" });
            DropIndex("dbo.SchoolClassAdvertisements", new[] { "Advertisement_Id" });
            DropIndex("dbo.SchoolClassAdvertisements", new[] { "SchoolClass_Id" });
            DropIndex("dbo.Messages", new[] { "UserTo_Id" });
            DropIndex("dbo.Messages", new[] { "UserFrom_Id" });
            DropIndex("dbo.SubjectFiles", new[] { "Subject_Id" });
            DropIndex("dbo.Subjects", new[] { "Name" });
            DropIndex("dbo.QuestionAnswers", new[] { "Question_Id" });
            DropIndex("dbo.Questions", new[] { "Test_Id" });
            DropIndex("dbo.Tests", new[] { "Subject_Id" });
            DropIndex("dbo.SchoolGrades", new[] { "Teacher_Id" });
            DropIndex("dbo.SchoolGrades", new[] { "Subject_Id" });
            DropIndex("dbo.SchoolGrades", new[] { "Student_Id" });
            DropIndex("dbo.SchoolGrades", new[] { "MyTest_Id" });
            DropIndex("dbo.SchoolClasses", new[] { "Teacher_Id" });
            DropIndex("dbo.SchoolClasses", new[] { "Name" });
            DropIndex("dbo.Advertisements", new[] { "Teacher_Id" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropTable("dbo.Teacher");
            DropTable("dbo.Student");
            DropTable("dbo.Parent");
            DropTable("dbo.Admin");
            DropTable("dbo.TeacherSubjects");
            DropTable("dbo.SubjectSchoolClasses");
            DropTable("dbo.SchoolClassAdvertisements");
            DropTable("dbo.Messages");
            DropTable("dbo.SubjectFiles");
            DropTable("dbo.Subjects");
            DropTable("dbo.QuestionAnswers");
            DropTable("dbo.Questions");
            DropTable("dbo.Tests");
            DropTable("dbo.SchoolGrades");
            DropTable("dbo.SchoolClasses");
            DropTable("dbo.Advertisements");
            DropTable("dbo.Users");
        }
    }
}
