namespace KazanAirportWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForFlightSave : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flights", "StatusName", c => c.String(maxLength: 50));
            DropColumn("dbo.Flights", "StatusId");
            DropColumn("dbo.Planes", "Number");
            DropColumn("dbo.Planes", "SeatsNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Planes", "SeatsNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Planes", "Number", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Flights", "StatusId", c => c.Int(nullable: false));
            DropColumn("dbo.Flights", "StatusName");
        }
    }
}
