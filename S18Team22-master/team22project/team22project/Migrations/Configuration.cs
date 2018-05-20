namespace team22project.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using team22project.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<team22project.DAL.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(team22project.DAL.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            SeedIdentity si = new SeedIdentity();
            si.AddAdmin(context);

            GenreData AddGenres = new GenreData();
            AddGenres.SeedGenres(context);

            MovieData AddMovies = new MovieData();
            AddMovies.SeedMovies(context);

            CustomerData AddCustomers = new CustomerData();
            AddCustomers.SeedCustomers(context);

            EmployeeData AddEmployees = new EmployeeData();
            AddEmployees.SeedEmployees(context);

            ShowingData AddShowing = new ShowingData();
            AddShowing.SeedShowings(context);

            //SeatData AddSeats = new SeatData();
            //AddSeats.SeedSeats(context);

          
        }
    }
}
