namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProduct2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SaleDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.SaleDetails", new[] { "ProductId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.SaleDetails", "ProductId");
            AddForeignKey("dbo.SaleDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
