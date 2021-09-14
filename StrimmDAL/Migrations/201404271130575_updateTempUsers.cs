namespace StrimmDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class updateTempUsers : DbMigration
    {

        public override void Up()
        {
            AddColumn("dbo.TempUsers", "userName", c => c.String());
        }

    }


}

