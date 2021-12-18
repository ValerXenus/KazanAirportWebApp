namespace KazanAirportWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airlines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                        IcaoCode = c.String(maxLength: 4, unicode: false),
                        IataCode = c.String(maxLength: 3, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FlightNumber = c.String(maxLength: 20, unicode: false),
                        DepartureScheduled = c.DateTime(nullable: false),
                        ArrivalScheduled = c.DateTime(nullable: false),
                        DepartureActual = c.DateTime(),
                        ArrivalActual = c.DateTime(),
                        TimeOnBoard = c.Int(nullable: false),
                        FlightType = c.Int(nullable: false),
                        PlaneId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(maxLength: 50, unicode: false),
                        FirstName = c.String(maxLength: 50, unicode: false),
                        MiddleName = c.String(maxLength: 50, unicode: false),
                        PassportNumber = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Planes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                        Number = c.String(maxLength: 50, unicode: false),
                        SeatsNumber = c.Int(nullable: false),
                        AirlineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserLogin = c.String(maxLength: 50, unicode: false),
                        UserPassword = c.String(maxLength: 50, unicode: false),
                        Email = c.String(maxLength: 50, unicode: false),
                        PassengerId = c.Int(),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Planes");
            DropTable("dbo.Passengers");
            DropTable("dbo.Flights");
            DropTable("dbo.Cities");
            DropTable("dbo.Airlines");
        }
    }
}
