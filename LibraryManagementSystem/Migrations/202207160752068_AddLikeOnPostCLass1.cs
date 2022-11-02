namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikeOnPostCLass1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LikeOnPosts", "PostId_Id", "dbo.Posts");
            DropIndex("dbo.LikeOnPosts", new[] { "PostId_Id" });
            RenameColumn(table: "dbo.LikeOnPosts", name: "PostId_Id", newName: "PostId");
            AlterColumn("dbo.LikeOnPosts", "PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.LikeOnPosts", "PostId");
            AddForeignKey("dbo.LikeOnPosts", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeOnPosts", "PostId", "dbo.Posts");
            DropIndex("dbo.LikeOnPosts", new[] { "PostId" });
            AlterColumn("dbo.LikeOnPosts", "PostId", c => c.Int());
            RenameColumn(table: "dbo.LikeOnPosts", name: "PostId", newName: "PostId_Id");
            CreateIndex("dbo.LikeOnPosts", "PostId_Id");
            AddForeignKey("dbo.LikeOnPosts", "PostId_Id", "dbo.Posts", "Id");
        }
    }
}
