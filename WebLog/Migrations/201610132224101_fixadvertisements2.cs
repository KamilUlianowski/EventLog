namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixadvertisements2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SchoolClasses", "Advertisement_Id", "dbo.Advertisements");
            DropIndex("dbo.SchoolClasses", new[] { "Advertisement_Id" });
            CreateTable(
                "dbo.SchoolClassAdvertisements",
                c => new
                    {
                        SchoolClass_Id = c.Int(nullable: false),
                        Advertisement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchoolClass_Id, t.Advertisement_Id })
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClass_Id, cascadeDelete: true)
                .ForeignKey("dbo.Advertisements", t => t.Advertisement_Id, cascadeDelete: true)
                .Index(t => t.SchoolClass_Id)
                .Index(t => t.Advertisement_Id);
            
            DropColumn("dbo.SchoolClasses", "Advertisement_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchoolClasses", "Advertisement_Id", c => c.Int());
            DropForeignKey("dbo.SchoolClassAdvertisements", "Advertisement_Id", "dbo.Advertisements");
            DropForeignKey("dbo.SchoolClassAdvertisements", "SchoolClass_Id", "dbo.SchoolClasses");
            DropIndex("dbo.SchoolClassAdvertisements", new[] { "Advertisement_Id" });
            DropIndex("dbo.SchoolClassAdvertisements", new[] { "SchoolClass_Id" });
            DropTable("dbo.SchoolClassAdvertisements");
            CreateIndex("dbo.SchoolClasses", "Advertisement_Id");
            AddForeignKey("dbo.SchoolClasses", "Advertisement_Id", "dbo.Advertisements", "Id");
        }
    }
}
