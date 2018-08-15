namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SaleDetails", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SaleDetails", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SaleDetails", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Sales", "GrossAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "GrossAmount", c => c.Single(nullable: false));
            AlterColumn("dbo.SaleDetails", "Cost", c => c.Single(nullable: false));
            AlterColumn("dbo.SaleDetails", "UnitPrice", c => c.Single(nullable: false));
            AlterColumn("dbo.SaleDetails", "Quantity", c => c.Single(nullable: false));
            AlterColumn("dbo.Products", "Quantity", c => c.Single(nullable: false));
        }
    }
}
