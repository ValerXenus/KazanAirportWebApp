namespace KazanAirportWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketsFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "SeatNumber", c => c.String(maxLength: 5, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "SeatNumber", c => c.String(maxLength: 2, unicode: false));
        }
    }
}
