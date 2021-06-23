namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_changeWriterMail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Writers", "WriterMail", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Writers", "WriterMail", c => c.Binary());
        }
    }
}
