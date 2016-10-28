namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Birth");
            DropColumn("dbo.Users", "Adress");
            DropColumn("dbo.Users", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Phone", c => c.String());
            AddColumn("dbo.Users", "Adress", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Birth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
