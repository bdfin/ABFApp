namespace ABF.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationsToEventTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "Name", c => c.String());
        }
    }
}
