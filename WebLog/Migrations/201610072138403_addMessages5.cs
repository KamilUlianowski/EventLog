namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMessages5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Message_Id", "dbo.Messages");
            DropIndex("dbo.Messages", new[] { "Message_Id" });
            AddColumn("dbo.Messages", "User_Id", c => c.Int());
            CreateIndex("dbo.Messages", "User_Id");
            AddForeignKey("dbo.Messages", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Messages", "Message_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Message_Id", c => c.Int());
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropColumn("dbo.Messages", "User_Id");
            CreateIndex("dbo.Messages", "Message_Id");
            AddForeignKey("dbo.Messages", "Message_Id", "dbo.Messages", "Id");
        }
    }
}
