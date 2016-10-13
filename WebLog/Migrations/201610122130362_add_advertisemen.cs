namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_advertisemen : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SubjectSchoolClasses", newName: "SchoolClassSubjects");
            RenameTable(name: "dbo.TeacherSubjects", newName: "SubjectTeachers");
            DropPrimaryKey("dbo.SchoolClassSubjects");
            DropPrimaryKey("dbo.SubjectTeachers");
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            AddPrimaryKey("dbo.SchoolClassSubjects", new[] { "SchoolClass_Id", "Subject_Id" });
            AddPrimaryKey("dbo.SubjectTeachers", new[] { "Subject_Id", "Teacher_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "Teacher_Id", "dbo.Teacher");
            DropIndex("dbo.Advertisements", new[] { "Teacher_Id" });
            DropPrimaryKey("dbo.SubjectTeachers");
            DropPrimaryKey("dbo.SchoolClassSubjects");
            DropTable("dbo.Advertisements");
            AddPrimaryKey("dbo.SubjectTeachers", new[] { "Teacher_Id", "Subject_Id" });
            AddPrimaryKey("dbo.SchoolClassSubjects", new[] { "Subject_Id", "SchoolClass_Id" });
            RenameTable(name: "dbo.SubjectTeachers", newName: "TeacherSubjects");
            RenameTable(name: "dbo.SchoolClassSubjects", newName: "SubjectSchoolClasses");
        }
    }
}
