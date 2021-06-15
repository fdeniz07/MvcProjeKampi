namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addedMessage_isImportant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "IsImportant", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "IsImportant");
        }
    }
}
