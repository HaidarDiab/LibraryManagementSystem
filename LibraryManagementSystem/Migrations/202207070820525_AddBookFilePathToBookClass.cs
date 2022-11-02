namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookFilePathToBookClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BookFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "BookFilePath");
        }
    }
}
