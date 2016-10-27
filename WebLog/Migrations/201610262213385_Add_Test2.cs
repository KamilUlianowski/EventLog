namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Test2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Name", c => c.String());
            AddColumn("dbo.Tests", "Subject_Id", c => c.Int());
            CreateIndex("dbo.Tests", "Subject_Id");
            AddForeignKey("dbo.Tests", "Subject_Id", "dbo.Subjects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Tests", new[] { "Subject_Id" });
            DropColumn("dbo.Tests", "Subject_Id");
            DropColumn("dbo.Tests", "Name");
        }
    }
}
