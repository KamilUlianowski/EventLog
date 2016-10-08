namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMessages3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "UserFrom_Id", c => c.Int());
            AddColumn("dbo.Messages", "UserTo_Id", c => c.Int());
            CreateIndex("dbo.Messages", "UserFrom_Id");
            CreateIndex("dbo.Messages", "UserTo_Id");
            AddForeignKey("dbo.Messages", "UserFrom_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Messages", "UserTo_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserTo_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "UserFrom_Id", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "UserTo_Id" });
            DropIndex("dbo.Messages", new[] { "UserFrom_Id" });
            DropColumn("dbo.Messages", "UserTo_Id");
            DropColumn("dbo.Messages", "UserFrom_Id");
        }
    }
}
