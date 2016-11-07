namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDatabasexxx8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "Hide", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "Hide");
        }
    }
}
