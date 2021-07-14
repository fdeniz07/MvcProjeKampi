namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_Status_in_Category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "StatusId", c => c.Int());
            CreateIndex("dbo.Categories", "StatusId");
            AddForeignKey("dbo.Categories", "StatusId", "dbo.Status", "StatusId");
            DropColumn("dbo.Categories", "CategoryStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "CategoryStatus", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Categories", "StatusId", "dbo.Status");
            DropIndex("dbo.Categories", new[] { "StatusId" });
            DropColumn("dbo.Categories", "StatusId");
        }
    }
}
