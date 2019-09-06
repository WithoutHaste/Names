namespace NameEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Name",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsFamiliar = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NameDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameId = c.Int(nullable: false),
                        SourceId = c.Int(nullable: false),
                        IsBoy = c.Boolean(),
                        IsGirl = c.Boolean(),
                        IsFirstName = c.Boolean(),
                        IsLastName = c.Boolean(),
                        Origin = c.String(),
                        Meaning = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Name", t => t.NameId, cascadeDelete: true)
                .ForeignKey("dbo.Source", t => t.SourceId, cascadeDelete: true)
                .Index(t => t.NameId)
                .Index(t => t.SourceId);
            
            CreateTable(
                "dbo.Source",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Spelling",
                c => new
                    {
                        CommonNameId = c.Int(nullable: false),
                        VariationNameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommonNameId, t.VariationNameId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NameDetail", "SourceId", "dbo.Source");
            DropForeignKey("dbo.NameDetail", "NameId", "dbo.Name");
            DropIndex("dbo.NameDetail", new[] { "SourceId" });
            DropIndex("dbo.NameDetail", new[] { "NameId" });
            DropTable("dbo.Spelling");
            DropTable("dbo.Source");
            DropTable("dbo.NameDetail");
            DropTable("dbo.Name");
        }
    }
}
