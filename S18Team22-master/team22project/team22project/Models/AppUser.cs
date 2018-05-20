using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

//TODO: Change this namespace to match your project
namespace team22project.Models
{
    public enum EmployeeType { Employee, Manager }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    //NOTE: This is the class for users
    public class AppUser : IdentityUser
    {
        //TODO: Put any additional fields that you need for your user here
        //First name is here as an example

        public Int32 CustomerNumber { get; set; }

        public EmployeeType EmployeeType { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name ="Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [Display(Name ="Address")]
        public String Address { get; set; }

        [MinimumAge(13)]
        [Required(ErrorMessage = "Date of birth is required.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }


        public Int32 PopcornPoints { get; set; }

        //public string CreditCards { get; set; }
        public String CreditCard1 { get; set; }

        public String CreditCard2 { get; set; }



        //TODO: Add any navigational properties needed for your user
        //Orders is here as an example
        public virtual List<Transaction> Transactions { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public AppUser()
        {
            if (Transactions == null)
            {
                Transactions = new List<Transaction>();
            }
            if (Reviews == null)
            {
                Reviews = new List<Review>();
            }

        }

        //This method allows you to create a new user
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // NOTE: The authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public class MinimumAgeAttribute : ValidationAttribute
        {
            int _minimumAge;

            public MinimumAgeAttribute(int minimumAge)
            {
                _minimumAge = minimumAge;
            }

            public override bool IsValid(object value)
            {
                DateTime date;
                if (DateTime.TryParse(value.ToString(), out date))
                {
                    return date.AddYears(_minimumAge) < DateTime.Now;
                }

                return false;
            }
        }

        }
    }