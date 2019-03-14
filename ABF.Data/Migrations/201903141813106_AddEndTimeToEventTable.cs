namespace ABF.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEndTimeToEventTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Event", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Event", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Event", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.Event", "EndTime");
            DropColumn("dbo.Event", "StartTime");
        }
    }
}
