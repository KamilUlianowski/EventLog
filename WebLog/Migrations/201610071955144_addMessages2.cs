namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMessages2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Student_Id", "dbo.Student");
            DropIndex("dbo.Messages", new[] { "Student_Id" });
            DropColumn("dbo.Messages", "Student_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Student_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Student_Id");
            AddForeignKey("dbo.Messages", "Student_Id", "dbo.Student", "Id");
        }
    }
}
