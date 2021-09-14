namespace StrimmDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateScheduleList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleLists", "description", c => c.String());
            AddColumn("dbo.ScheduleLists", "videoId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleLists", "videoId");
            DropColumn("dbo.ScheduleLists", "description");
        }
    }
}
