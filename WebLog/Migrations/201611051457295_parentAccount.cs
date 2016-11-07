namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parentAccount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parent", "Student_Id", "dbo.Student");
            DropIndex("dbo.Parent", new[] { "Student_Id" });
            AddColumn("dbo.Student", "Parent_Id", c => c.Int());
            CreateIndex("dbo.Student", "Parent_Id");
            AddForeignKey("dbo.Student", "Parent_Id", "dbo.Parent", "Id");
            DropColumn("dbo.Parent", "Student_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parent", "Student_Id", c => c.Int());
            DropForeignKey("dbo.Student", "Parent_Id", "dbo.Parent");
            DropIndex("dbo.Student", new[] { "Parent_Id" });
            DropColumn("dbo.Student", "Parent_Id");
            CreateIndex("dbo.Parent", "Student_Id");
            AddForeignKey("dbo.Parent", "Student_Id", "dbo.Student", "Id");
        }
    }
}
