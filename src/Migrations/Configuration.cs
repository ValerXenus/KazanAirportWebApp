using System.Data.Entity.Migrations;

namespace KazanAirportWebApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<KazanAirportWebApp.DataAccess.KazanAirportDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KazanAirportWebApp.DataAccess.KazanAirportDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
