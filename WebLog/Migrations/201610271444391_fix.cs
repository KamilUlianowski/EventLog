namespace WebLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.QuestionAnswers", name: "Test_Id", newName: "Question_Id");
            RenameIndex(table: "dbo.QuestionAnswers", name: "IX_Test_Id", newName: "IX_Question_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.QuestionAnswers", name: "IX_Question_Id", newName: "IX_Test_Id");
            RenameColumn(table: "dbo.QuestionAnswers", name: "Question_Id", newName: "Test_Id");
        }
    }
}
