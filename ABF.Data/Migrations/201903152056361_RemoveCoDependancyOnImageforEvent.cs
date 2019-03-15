namespace ABF.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCoDependancyOnImageforEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Image", "Event_Id", "dbo.Event");
            DropIndex("dbo.Image", new[] { "Event_Id" });
            AlterColumn("dbo.Event", "ImageId", c => c.Int(nullable: false));
            DropColumn("dbo.Image", "Event_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Image", "Event_Id", c => c.Int());
            AlterColumn("dbo.Event", "ImageId", c => c.Int());
            CreateIndex("dbo.Image", "Event_Id");
            AddForeignKey("dbo.Image", "Event_Id", "dbo.Event", "Id");
        }
    }
}
