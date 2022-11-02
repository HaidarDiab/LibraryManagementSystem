namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyDataAnnotaionOnCategoryTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BooksCategories", "Name", c => c.String(nullable: false, maxLength: 26));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BooksCategories", "Name", c => c.String(nullable: false));
        }
    }
}
