namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDtaeTimePropertyToCommentClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CommentDateAdded", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CommentDateAdded");
        }
    }
}
