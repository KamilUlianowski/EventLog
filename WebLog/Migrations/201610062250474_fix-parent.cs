namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixparent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parent", "Parent_Id", "dbo.Parent");
            DropIndex("dbo.Parent", new[] { "Parent_Id" });
            AddColumn("dbo.Parent", "Student_Id", c => c.Int());
            CreateIndex("dbo.Parent", "Student_Id");
            AddForeignKey("dbo.Parent", "Student_Id", "dbo.Parent", "Id");
            DropColumn("dbo.Parent", "Parent_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parent", "Parent_Id", c => c.Int());
            DropForeignKey("dbo.Parent", "Student_Id", "dbo.Parent");
            DropIndex("dbo.Parent", new[] { "Student_Id" });
            DropColumn("dbo.Parent", "Student_Id");
            CreateIndex("dbo.Parent", "Parent_Id");
            AddForeignKey("dbo.Parent", "Parent_Id", "dbo.Parent", "Id");
        }
    }
}
