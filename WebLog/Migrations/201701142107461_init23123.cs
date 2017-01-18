namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init23123 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "SchoolClass_Id", "dbo.SchoolClasses");
            DropIndex("dbo.Events", new[] { "SchoolClass_Id" });
            DropColumn("dbo.Events", "SchoolClass_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "SchoolClass_Id", c => c.Int());
            CreateIndex("dbo.Events", "SchoolClass_Id");
            AddForeignKey("dbo.Events", "SchoolClass_Id", "dbo.SchoolClasses", "Id");
        }
    }
}
