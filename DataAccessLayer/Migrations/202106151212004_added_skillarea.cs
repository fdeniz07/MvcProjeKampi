namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_skillarea : DbMigration
    {
        public override void Up()
        {
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
            DropIndex("dbo.Talents", new[] { "SkillAreaId" });
            DropTable("dbo.Talents");
            DropTable("dbo.SkillAreas");
        }
    }
}
