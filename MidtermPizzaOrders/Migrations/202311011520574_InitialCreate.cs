namespace MidtermPizzaOrders.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PizzaOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientName = c.String(nullable: false, maxLength: 100),
                        ClientPostalCode = c.String(nullable: false),
                        DeliveryDeadline = c.DateTime(nullable: false),
                        Size = c.Int(nullable: false),
                        BakingTimeMinutes = c.Int(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PizzaOrders");
        }
    }
}
