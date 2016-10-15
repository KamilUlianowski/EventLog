namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enumForGrades2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subjects", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.SubjectFiles", "Path", c => c.String(nullable: false));
            CreateIndex("dbo.SchoolClasses", "Name", unique: true);
            CreateIndex("dbo.Users", "Email", unique: true);
            CreateIndex("dbo.Subjects", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Subjects", new[] { "Name" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.SchoolClasses", new[] { "Name" });
            AlterColumn("dbo.SubjectFiles", "Path", c => c.String());
            AlterColumn("dbo.Subjects", "Name", c => c.String());
        }
    }
}
