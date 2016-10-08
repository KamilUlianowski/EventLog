namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationBetweenParentAndStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parent", "Parent_Id", c => c.Int());
            CreateIndex("dbo.Parent", "Parent_Id");
            AddForeignKey("dbo.Parent", "Parent_Id", "dbo.Parent", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parent", "Parent_Id", "dbo.Parent");
            DropIndex("dbo.Parent", new[] { "Parent_Id" });
            DropColumn("dbo.Parent", "Parent_Id");
        }
    }
}
