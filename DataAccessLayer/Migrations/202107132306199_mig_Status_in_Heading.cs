namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_Status_in_Heading : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Headings", "StatusId", c => c.Int());
            CreateIndex("dbo.Headings", "StatusId");
            AddForeignKey("dbo.Headings", "StatusId", "dbo.Status", "StatusId");
            DropColumn("dbo.Headings", "HeadingStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Headings", "HeadingStatus", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Headings", "StatusId", "dbo.Status");
            DropIndex("dbo.Headings", new[] { "StatusId" });
            DropColumn("dbo.Headings", "StatusId");
        }
    }
}
