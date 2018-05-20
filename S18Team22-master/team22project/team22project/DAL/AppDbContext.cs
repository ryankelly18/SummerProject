using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

//TODO: Change this using statement to match your project
using team22project.Models;


//TODO: Change this namespace to match your project
namespace team22project.DAL
{
    // NOTE: Here's your db context for the project.  All of your db sets should go in here
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //TODO: Make sure that your connection string name is correct here.
        public AppDbContext()
            : base("MyDBConnection", throwIfV1Schema: false) { }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        // TODO: Add dbsets here. Remember, Identity adds a db set for users, 
        //so you shouldn't add that one - you will get an error
        //here's a sample for products

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }

        //public DbSet<Seat> Seats { get; set; }

        public DbSet<Showing> Showings { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }


        //NOTE: This is a dbSet that you need to make roles work
        public DbSet<AppRole> AppRoles { get; set; }


    }
}