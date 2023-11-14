namespace MidtermPizzaOrders.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PizzaOrders", "Extras", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PizzaOrders", "Extras");
        }
    }
}
