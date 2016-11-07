namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDatabasexxx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "OnlyForParents", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "OnlyForParents");
        }
    }
}
