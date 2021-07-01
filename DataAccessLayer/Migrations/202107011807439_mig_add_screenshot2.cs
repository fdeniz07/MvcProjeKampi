namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_add_screenshot2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScreenShots",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(maxLength: 100),
                        ImagePath = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ImageId);
            
            AlterColumn("dbo.Writers", "WriterMail", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Writers", "WriterMail", c => c.Binary());
            DropTable("dbo.ScreenShots");
        }
    }
}
