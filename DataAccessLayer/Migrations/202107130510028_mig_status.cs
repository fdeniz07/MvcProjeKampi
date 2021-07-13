namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_status : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
            AddColumn("dbo.Admins", "AdminStatusId", c => c.Int());
            AddColumn("dbo.Admins", "Status_StatusId", c => c.Int());
            CreateIndex("dbo.Admins", "Status_StatusId");
            AddForeignKey("dbo.Admins", "Status_StatusId", "dbo.Status", "StatusId");
            DropColumn("dbo.Admins", "AdminStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "AdminStatus", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Admins", "Status_StatusId", "dbo.Status");
            DropIndex("dbo.Admins", new[] { "Status_StatusId" });
            DropColumn("dbo.Admins", "Status_StatusId");
            DropColumn("dbo.Admins", "AdminStatusId");
            DropTable("dbo.Status");
        }
    }
}
