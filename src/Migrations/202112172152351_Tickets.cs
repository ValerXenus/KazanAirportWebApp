namespace KazanAirportWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketNumber = c.String(maxLength: 10, unicode: false),
                        Price = c.Double(nullable: false),
                        PassengerId = c.Int(nullable: false),
                        FlightId = c.Int(nullable: false),
                        SeatNumber = c.String(maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Flights", "FlightType", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Flights", "FlightType", c => c.Int(nullable: false));
            DropTable("dbo.Tickets");
        }
    }
}
