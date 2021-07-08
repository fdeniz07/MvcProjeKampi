namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addImageDate_inImageFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageFiles", "ImageDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageFiles", "ImageDate");
        }
    }
}
