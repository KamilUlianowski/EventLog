namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sfs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "Title", c => c.String());
            AlterColumn("dbo.Advertisements", "Text", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Advertisements", "Text", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.Advertisements", "Title");
        }
    }
}
