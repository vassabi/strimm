namespace StrimmDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isLocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}
