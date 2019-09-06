namespace NameEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Category = c.String(nullable: false, maxLength: 128),
                        SuperCategory = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Category);
            
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
                        Origin = c.String(maxLength: 128),
                        Meaning = c.String(maxLength: 256),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Name", t => t.NameId, cascadeDelete: true)
                .ForeignKey("dbo.Source", t => t.SourceId, cascadeDelete: true)
                .Index(t => t.NameId)
                .Index(t => t.SourceId);
            
            CreateTable(
                "dbo.Name",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        FirstLetter = c.String(nullable: false, maxLength: 1),
                        IsFamiliar = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Source",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Url = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NickName",
                c => new
                    {
                        NickNameId = c.Int(nullable: false),
                        FullNameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NickNameId, t.FullNameId });
            
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
            DropTable("dbo.NickName");
            DropTable("dbo.Source");
            DropTable("dbo.Name");
            DropTable("dbo.NameDetail");
            DropTable("dbo.Category");
        }
    }
}
