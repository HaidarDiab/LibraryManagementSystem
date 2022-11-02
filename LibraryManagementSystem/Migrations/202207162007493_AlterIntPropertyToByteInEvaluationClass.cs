namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterIntPropertyToByteInEvaluationClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Evaluations", "Level", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Evaluations", "Level", c => c.Int(nullable: false));
        }
    }
}
