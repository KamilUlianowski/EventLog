namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Advertisements", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Advertisements", "Text", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
