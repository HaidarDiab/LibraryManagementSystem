namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavouriteBookListClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavouriteBookLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavouriteBookLists", "BookId", "dbo.Books");
            DropForeignKey("dbo.FavouriteBookLists", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.FavouriteBookLists", new[] { "ApplicationUserId" });
            DropIndex("dbo.FavouriteBookLists", new[] { "BookId" });
            DropTable("dbo.FavouriteBookLists");
        }
    }
}
