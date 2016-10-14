namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixadvertisements : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SubjectTeachers", newName: "TeacherSubjects");
            RenameTable(name: "dbo.SchoolClassSubjects", newName: "SubjectSchoolClasses");
            DropPrimaryKey("dbo.TeacherSubjects");
            DropPrimaryKey("dbo.SubjectSchoolClasses");
            AddColumn("dbo.SchoolClasses", "Advertisement_Id", c => c.Int());
            AddPrimaryKey("dbo.TeacherSubjects", new[] { "Teacher_Id", "Subject_Id" });
            AddPrimaryKey("dbo.SubjectSchoolClasses", new[] { "Subject_Id", "SchoolClass_Id" });
            CreateIndex("dbo.SchoolClasses", "Advertisement_Id");
            AddForeignKey("dbo.SchoolClasses", "Advertisement_Id", "dbo.Advertisements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolClasses", "Advertisement_Id", "dbo.Advertisements");
            DropIndex("dbo.SchoolClasses", new[] { "Advertisement_Id" });
            DropPrimaryKey("dbo.SubjectSchoolClasses");
            DropPrimaryKey("dbo.TeacherSubjects");
            DropColumn("dbo.SchoolClasses", "Advertisement_Id");
            AddPrimaryKey("dbo.SubjectSchoolClasses", new[] { "SchoolClass_Id", "Subject_Id" });
            AddPrimaryKey("dbo.TeacherSubjects", new[] { "Subject_Id", "Teacher_Id" });
            RenameTable(name: "dbo.SubjectSchoolClasses", newName: "SchoolClassSubjects");
            RenameTable(name: "dbo.TeacherSubjects", newName: "SubjectTeachers");
        }
    }
}
