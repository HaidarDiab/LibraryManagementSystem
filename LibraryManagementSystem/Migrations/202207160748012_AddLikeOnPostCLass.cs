namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikeOnPostCLass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikeOnPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Like = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        PostId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Posts", t => t.PostId_Id)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.PostId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeOnPosts", "PostId_Id", "dbo.Posts");
            DropForeignKey("dbo.LikeOnPosts", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.LikeOnPosts", new[] { "PostId_Id" });
            DropIndex("dbo.LikeOnPosts", new[] { "ApplicationUserId" });
            DropTable("dbo.LikeOnPosts");
        }
    }
}
