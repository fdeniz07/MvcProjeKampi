namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_CategoryDate_in_category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CategoryDate");
        }
    }
}
