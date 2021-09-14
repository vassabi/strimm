namespace StrimmDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublicLibs",
                c => new
                {
                    videoUploadId = c.Int(nullable: false, identity: true),
                    title = c.String(),
                    description = c.String(),
                    videoPath = c.String(),
                    duration = c.Double(nullable: false),
                    videoThumbnail = c.String(),
                    videoCount = c.Long(nullable: false),
                    isScheduled = c.Boolean(nullable: false),
                    useCount = c.Int(nullable: false),
                    categoryId = c.Int(nullable: false),
                    provider = c.String(),
                    addedDate = c.DateTime(nullable: false),
                    r_rated = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.videoUploadId)
                .ForeignKey("dbo.ChannelCategories", t => t.categoryId, cascadeDelete: true)
                .Index(t => t.categoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublicLibs", "categoryId", "dbo.ChannelCategories");
            DropIndex("dbo.PublicLibs", new[] { "categoryId" });
            DropTable("dbo.PublicLibs");
        }
    }
}
