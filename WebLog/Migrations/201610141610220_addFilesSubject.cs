namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFilesSubject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubjectFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectFiles", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.SubjectFiles", new[] { "Subject_Id" });
            DropTable("dbo.SubjectFiles");
        }
    }
}
