namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProduct3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "PaymentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "PaymentType");
        }
    }
}
