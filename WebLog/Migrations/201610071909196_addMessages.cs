namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        Teacher_Id = c.Int(),
                        Student_Id = c.Int(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id)
                .ForeignKey("dbo.Parent", t => t.Student_Id)
                .ForeignKey("dbo.Parent", t => t.Parent_Id)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Parent_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Parent_Id", "dbo.Parent");
            DropForeignKey("dbo.Messages", "Student_Id", "dbo.Parent");
            DropForeignKey("dbo.Messages", "Teacher_Id", "dbo.Teacher");
            DropIndex("dbo.Messages", new[] { "Parent_Id" });
            DropIndex("dbo.Messages", new[] { "Student_Id" });
            DropIndex("dbo.Messages", new[] { "Teacher_Id" });
            DropTable("dbo.Messages");
        }
    }
}
