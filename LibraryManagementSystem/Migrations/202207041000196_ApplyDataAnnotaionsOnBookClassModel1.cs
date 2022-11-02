namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyDataAnnotaionsOnBookClassModel1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 44));
            AlterColumn("dbo.Books", "Summary", c => c.String(nullable: false, maxLength: 415));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Summary", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
        }
    }
}
