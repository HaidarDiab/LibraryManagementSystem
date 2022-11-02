namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyDataAnnotaionsOnBookClassModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Summary", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "ReleaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Books", "AuthorName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "AuthorName", c => c.String());
            AlterColumn("dbo.Books", "ReleaseDate", c => c.DateTime());
            AlterColumn("dbo.Books", "Summary", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
        }
    }
}
