using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

using team22project.DAL;
using team22project.Models;
using System;

namespace team22project.Migrations
{
    //add identity data
    public class SeedIdentity
    {
        public void AddAdmin(AppDbContext db)
        {
            //create a user manager and a role manager to use for this method
            AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));

            //create a role manager
            AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));


            //check to see if the manager has been added
            AppUser manager = db.Users.FirstOrDefault(u => u.Email == "admin@example.com");

            //if manager hasn't been created, then add them
            if (manager == null)
            {
                //TODO: Add any additional fields for user here
                manager = new AppUser();
                manager.UserName = "admin@example.com";
                manager.Email = "admin@example.com";
                manager.FirstName = "Admin";
                manager.LastName = "Admin";
                manager.PhoneNumber = "(512)555-5555";
                manager.Birthday = Convert.ToDateTime("01/01/1990");
                manager.Address = "1000 Guadalupe St";

                var result = UserManager.Create(manager, "Abc123!");
                db.SaveChanges();
                manager = db.Users.First(u => u.UserName == "admin@example.com");

            }
            


            //TODO: Add the needed roles
            //if role doesn't exist, add it
            if (RoleManager.RoleExists("Manager") == false)
            {
                RoleManager.Create(new AppRole("Manager"));
            }

            if (RoleManager.RoleExists("Customer") == false)
            {
                RoleManager.Create(new AppRole("Customer"));
            }

            if(RoleManager.RoleExists("Employee") == false)
            {
                RoleManager.Create(new AppRole("Employee"));
            }


            //make sure user is in role
            if (UserManager.IsInRole(manager.Id, "Manager") == false)
            {
                UserManager.AddToRole(manager.Id, "Manager");
            }

            //save changes
            db.SaveChanges();
        }

    }
}