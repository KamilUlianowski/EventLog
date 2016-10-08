namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubjectStudents", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectStudents", "Student_Id", "dbo.Parent");
            DropIndex("dbo.SubjectStudents", new[] { "Subject_Id" });
            DropIndex("dbo.SubjectStudents", new[] { "Student_Id" });
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
            
            DropTable("dbo.SubjectStudents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubjectStudents",
                c => new
                    {
                        Subject_Id = c.Int(nullable: false),
                        Student_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subject_Id, t.Student_Id });
            
            DropForeignKey("dbo.SubjectSchoolClasses", "SchoolClass_Id", "dbo.SchoolClasses");
            DropForeignKey("dbo.SubjectSchoolClasses", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.SubjectSchoolClasses", new[] { "SchoolClass_Id" });
            DropIndex("dbo.SubjectSchoolClasses", new[] { "Subject_Id" });
            DropTable("dbo.SubjectSchoolClasses");
            CreateIndex("dbo.SubjectStudents", "Student_Id");
            CreateIndex("dbo.SubjectStudents", "Subject_Id");
            AddForeignKey("dbo.SubjectStudents", "Student_Id", "dbo.Parent", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SubjectStudents", "Subject_Id", "dbo.Subjects", "Id", cascadeDelete: true);
        }
    }
}
