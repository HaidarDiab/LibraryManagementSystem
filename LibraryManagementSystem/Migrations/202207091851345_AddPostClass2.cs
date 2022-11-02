namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostClass2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ApplicationUserId" });
            AddColumn("dbo.Posts", "ProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "ProfileId");
            AddForeignKey("dbo.Posts", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
            DropColumn("dbo.Posts", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ApplicationUserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Posts", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.Posts", new[] { "ProfileId" });
            DropColumn("dbo.Posts", "ProfileId");
            CreateIndex("dbo.Posts", "ApplicationUserId");
            AddForeignKey("dbo.Posts", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
