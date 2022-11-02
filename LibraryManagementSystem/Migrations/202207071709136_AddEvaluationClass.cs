namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEvaluationClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evaluations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.Byte(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evaluations", "BookId", "dbo.Books");
            DropForeignKey("dbo.Evaluations", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Evaluations", new[] { "BookId" });
            DropIndex("dbo.Evaluations", new[] { "ApplicationUserId" });
            DropTable("dbo.Evaluations");
        }
    }
}
