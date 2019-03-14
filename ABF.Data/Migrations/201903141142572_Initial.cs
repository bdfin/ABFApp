namespace ABF.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddOn",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Type = c.String(),
                        Price = c.Double(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Name = c.String(),
                        Details = c.String(),
                        Description = c.String(),
                        Capacity = c.Int(nullable: false),
                        IsMemberOnly = c.Boolean(nullable: false),
                        LocationId = c.Int(nullable: false),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        PostCode = c.String(),
                        DisabledAccess = c.Boolean(nullable: false),
                        Info = c.String(),
                        Contact = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        EventId = c.Int(nullable: false),
                        IsChild = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Order_Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.CustomerId)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        PostCode = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        MembershipId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Membership",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type = c.String(),
                        DatePurchased = c.DateTime(nullable: false),
                        Expiry = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Payment", t => t.PaymentId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.PaymentId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Method = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "EventId", "dbo.Event");
            DropForeignKey("dbo.Ticket", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Ticket", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.Order", "PaymentId", "dbo.Payment");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Membership", "Id", "dbo.Customer");
            DropForeignKey("dbo.Event", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Image", "Id", "dbo.Event");
            DropForeignKey("dbo.AddOn", "EventId", "dbo.Event");
            DropIndex("dbo.Order", new[] { "PaymentId" });
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.Membership", new[] { "Id" });
            DropIndex("dbo.Ticket", new[] { "Order_Id" });
            DropIndex("dbo.Ticket", new[] { "CustomerId" });
            DropIndex("dbo.Ticket", new[] { "EventId" });
            DropIndex("dbo.Image", new[] { "Id" });
            DropIndex("dbo.Event", new[] { "LocationId" });
            DropIndex("dbo.AddOn", new[] { "EventId" });
            DropTable("dbo.Payment");
            DropTable("dbo.Order");
            DropTable("dbo.Membership");
            DropTable("dbo.Customer");
            DropTable("dbo.Ticket");
            DropTable("dbo.Location");
            DropTable("dbo.Image");
            DropTable("dbo.Event");
            DropTable("dbo.AddOn");
        }
    }
}
