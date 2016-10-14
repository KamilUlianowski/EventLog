namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Content");
        }
    }
}
