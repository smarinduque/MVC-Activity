namespace MVCProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCustomerDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactName = c.String(maxLength: 250),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        Gender = c.String(maxLength: 10),
                        LocalAddress = c.String(maxLength: 256),
                        TelNo = c.String(maxLength: 150),
                        MobileNO = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(maxLength: 10),
                        OrderDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Orders_Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductCode = c.String(maxLength: 10),
                        ProductName = c.String(maxLength: 100),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        Name = c.String(maxLength: 50),
                        StockOnHand = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders_Items", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders_Items", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders_Items", new[] { "ProductId" });
            DropIndex("dbo.Orders_Items", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders_Items");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
