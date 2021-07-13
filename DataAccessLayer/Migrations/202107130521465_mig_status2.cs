namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_status2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Admins", name: "Status_StatusId", newName: "StatusId");
            RenameIndex(table: "dbo.Admins", name: "IX_Status_StatusId", newName: "IX_StatusId");
            DropColumn("dbo.Admins", "AdminStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "AdminStatusId", c => c.Int());
            RenameIndex(table: "dbo.Admins", name: "IX_StatusId", newName: "IX_Status_StatusId");
            RenameColumn(table: "dbo.Admins", name: "StatusId", newName: "Status_StatusId");
        }
    }
}
