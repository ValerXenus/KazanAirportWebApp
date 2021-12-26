namespace KazanAirportWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbFlightsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flights", "ScheduledDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Flights", "ActualDateTime", c => c.DateTime());
            DropColumn("dbo.Flights", "DepartureScheduled");
            DropColumn("dbo.Flights", "ArrivalScheduled");
            DropColumn("dbo.Flights", "DepartureActual");
            DropColumn("dbo.Flights", "ArrivalActual");
            DropColumn("dbo.Flights", "TimeOnBoard");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flights", "TimeOnBoard", c => c.Int(nullable: false));
            AddColumn("dbo.Flights", "ArrivalActual", c => c.DateTime());
            AddColumn("dbo.Flights", "DepartureActual", c => c.DateTime());
            AddColumn("dbo.Flights", "ArrivalScheduled", c => c.DateTime(nullable: false));
            AddColumn("dbo.Flights", "DepartureScheduled", c => c.DateTime(nullable: false));
            DropColumn("dbo.Flights", "ActualDateTime");
            DropColumn("dbo.Flights", "ScheduledDateTime");
        }
    }
}
