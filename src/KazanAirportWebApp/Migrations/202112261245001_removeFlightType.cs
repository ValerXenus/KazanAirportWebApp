namespace KazanAirportWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeFlightType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Flights", "FlightType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flights", "FlightType", c => c.Boolean(nullable: false));
        }
    }
}
