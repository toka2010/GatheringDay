namespace GatheringDay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Phone", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Phone", c => c.String());
        }
    }
}
