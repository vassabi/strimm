namespace StrimmDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateVideoTubes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoTubes", "isRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.VideoTubes", "isRestricted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideoTubes", "isRestricted");
            DropColumn("dbo.VideoTubes", "isRemoved");
        }
    }
}
