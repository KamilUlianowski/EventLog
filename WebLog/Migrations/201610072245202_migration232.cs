namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration232 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "SendDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "SendDate", c => c.DateTime(nullable: false));
        }
    }
}
