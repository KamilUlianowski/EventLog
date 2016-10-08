namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMessages4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Parent_Id", "dbo.Parent");
            DropForeignKey("dbo.Messages", "Teacher_Id", "dbo.Teacher");
            DropIndex("dbo.Messages", new[] { "Parent_Id" });
            DropIndex("dbo.Messages", new[] { "Teacher_Id" });
            AddColumn("dbo.Messages", "Message_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Message_Id");
            AddForeignKey("dbo.Messages", "Message_Id", "dbo.Messages", "Id");
            DropColumn("dbo.Messages", "Parent_Id");
            DropColumn("dbo.Messages", "Teacher_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Teacher_Id", c => c.Int());
            AddColumn("dbo.Messages", "Parent_Id", c => c.Int());
            DropForeignKey("dbo.Messages", "Message_Id", "dbo.Messages");
            DropIndex("dbo.Messages", new[] { "Message_Id" });
            DropColumn("dbo.Messages", "Message_Id");
            CreateIndex("dbo.Messages", "Teacher_Id");
            CreateIndex("dbo.Messages", "Parent_Id");
            AddForeignKey("dbo.Messages", "Teacher_Id", "dbo.Teacher", "Id");
            AddForeignKey("dbo.Messages", "Parent_Id", "dbo.Parent", "Id");
        }
    }
}
