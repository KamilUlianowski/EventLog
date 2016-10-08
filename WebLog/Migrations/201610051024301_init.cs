namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Birth = c.DateTime(nullable: false),
                        Email = c.String(),
                        Pesel = c.String(),
                        Adress = c.String(),
                        Phone = c.String(),
                        Password = c.String(),
                        TypeOfUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchoolClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SchoolClass_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.SchoolClasses", t => t.SchoolClass_Id)
                .Index(t => t.Id)
                .Index(t => t.SchoolClass_Id);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teacher", "Id", "dbo.Users");
            DropForeignKey("dbo.Parent", "SchoolClass_Id", "dbo.SchoolClasses");
            DropForeignKey("dbo.Parent", "Id", "dbo.Users");
            DropForeignKey("dbo.Parent", "Id", "dbo.Users");
            DropForeignKey("dbo.SchoolClasses", "Teacher_Id", "dbo.Teacher");
            DropIndex("dbo.Teacher", new[] { "Id" });
            DropIndex("dbo.Parent", new[] { "SchoolClass_Id" });
            DropIndex("dbo.Parent", new[] { "Id" });
            DropIndex("dbo.Parent", new[] { "Id" });
            DropIndex("dbo.SchoolClasses", new[] { "Teacher_Id" });
            DropTable("dbo.Teacher");
            DropTable("dbo.Parent");
            DropTable("dbo.Parent");
            DropTable("dbo.SchoolClasses");
            DropTable("dbo.Users");
        }
    }
}
