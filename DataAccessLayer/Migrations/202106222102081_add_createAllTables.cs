namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_createAllTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        AboutId = c.Int(nullable: false, identity: true),
                        AboutDetails1 = c.String(maxLength: 500),
                        AboutDetails2 = c.String(maxLength: 1000),
                        AboutImage1 = c.String(maxLength: 100),
                        AboutImage2 = c.String(maxLength: 100),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AboutId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        AdminUserName = c.String(maxLength: 50),
                        AdminMail = c.Binary(),
                        AdminPasswordHash = c.Binary(),
                        AdminPasswordSalt = c.Binary(),
                        AdminRole = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 50),
                        CategoryDescription = c.String(maxLength: 200),
                        CategoryStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Headings",
                c => new
                    {
                        HeadingId = c.Int(nullable: false, identity: true),
                        HeadingName = c.String(maxLength: 50),
                        HeadingDate = c.DateTime(nullable: false),
                        HeadingStatus = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        WriterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HeadingId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Writers", t => t.WriterId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.WriterId);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        ContentId = c.Int(nullable: false, identity: true),
                        ContentValue = c.String(maxLength: 1000),
                        ContentDate = c.DateTime(nullable: false),
                        ContentStatus = c.Boolean(nullable: false),
                        HeadingId = c.Int(nullable: false),
                        WriterId = c.Int(),
                    })
                .PrimaryKey(t => t.ContentId)
                .ForeignKey("dbo.Headings", t => t.HeadingId, cascadeDelete: true)
                .ForeignKey("dbo.Writers", t => t.WriterId)
                .Index(t => t.HeadingId)
                .Index(t => t.WriterId);
            
            CreateTable(
                "dbo.Writers",
                c => new
                    {
                        WriterId = c.Int(nullable: false, identity: true),
                        WriterUserName = c.String(maxLength: 50),
                        WriterName = c.String(maxLength: 50),
                        WriterSurName = c.String(maxLength: 50),
                        WriterImage = c.String(maxLength: 250),
                        WriterAbout = c.String(maxLength: 250),
                        WriterMail = c.Binary(),
                        WriterPasswordHash = c.Binary(),
                        WriterPasswordSalt = c.Binary(),
                        WriterTitle = c.String(maxLength: 50),
                        WriterStatus = c.Boolean(nullable: false),
                        WriterRole = c.String(),
                    })
                .PrimaryKey(t => t.WriterId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        UserMail = c.String(maxLength: 50),
                        Subject = c.String(maxLength: 50),
                        ContactDate = c.DateTime(nullable: false),
                        Message = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        IsImportant = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.ImageFiles",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(maxLength: 100),
                        ImagePath = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        SenderMail = c.String(maxLength: 50),
                        ReceiverMail = c.String(maxLength: 50),
                        Subject = c.String(maxLength: 100),
                        MessageContent = c.String(),
                        MessageDate = c.DateTime(nullable: false),
                        IsDraft = c.Boolean(nullable: false),
                        Trash = c.Boolean(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        IsImportant = c.Boolean(nullable: false),
                        IsSpam = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId);
            
            CreateTable(
                "dbo.SkillAreas",
                c => new
                    {
                        SkillAreaId = c.Int(nullable: false, identity: true),
                        Area = c.String(maxLength: 100),
                        AreaDetails = c.String(),
                    })
                .PrimaryKey(t => t.SkillAreaId);
            
            CreateTable(
                "dbo.Talents",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        SkillName = c.String(maxLength: 100),
                        SkillDetails = c.String(),
                        SkillLevel = c.Byte(nullable: false),
                        SkillAreaId = c.Int(),
                    })
                .PrimaryKey(t => t.SkillId)
                .ForeignKey("dbo.SkillAreas", t => t.SkillAreaId)
                .Index(t => t.SkillAreaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Talents", "SkillAreaId", "dbo.SkillAreas");
            DropForeignKey("dbo.Headings", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Contents", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Contents", "HeadingId", "dbo.Headings");
            DropForeignKey("dbo.Headings", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Talents", new[] { "SkillAreaId" });
            DropIndex("dbo.Contents", new[] { "WriterId" });
            DropIndex("dbo.Contents", new[] { "HeadingId" });
            DropIndex("dbo.Headings", new[] { "WriterId" });
            DropIndex("dbo.Headings", new[] { "CategoryId" });
            DropTable("dbo.Talents");
            DropTable("dbo.SkillAreas");
            DropTable("dbo.Messages");
            DropTable("dbo.ImageFiles");
            DropTable("dbo.Contacts");
            DropTable("dbo.Writers");
            DropTable("dbo.Contents");
            DropTable("dbo.Headings");
            DropTable("dbo.Categories");
            DropTable("dbo.Admins");
            DropTable("dbo.Abouts");
        }
    }
}
