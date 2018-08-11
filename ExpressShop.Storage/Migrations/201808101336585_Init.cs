namespace ExpressShop.Storage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 500),
                        Description = c.String(maxLength: 1000),
                        Characteristics = c.String(maxLength: 2000),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EntReservations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Characteristics = c.String(maxLength: 2000),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntProducts", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EntReservations", "ProductId", "dbo.EntProducts");
            DropIndex("dbo.EntReservations", new[] { "ProductId" });
            DropTable("dbo.EntReservations");
            DropTable("dbo.EntProducts");
        }
    }
}
