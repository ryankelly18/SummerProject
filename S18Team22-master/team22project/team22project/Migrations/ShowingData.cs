using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using team22project.DAL;
using team22project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace team22project.Migrations
{
    public class ShowingData
    {
        public void SeedShowings(AppDbContext db)
        {
            Showing r1 = new Showing();
            r1.Theatre = Theatre.Theatre1;
            r1.ShowingDate = DateTime.Parse("05/09/2018 09:05:00 AM").ToUniversalTime();
           // r1.StartTime = new TimeSpan(9, 05, 0);
            r1.Display = r1.ShowingDate.ToLocalTime();
            r1.DayOfWeek = r1.ShowingDate.DayOfWeek;
            r1.Movie = (db.Movies.FirstOrDefault(m => m.Title == "The Sting"));
            db.Showings.AddOrUpdate(r1);
            db.SaveChanges();

            Showing r2 = new Showing();
            r2.Theatre = Theatre.Theatre1;
            r2.ShowingDate = DateTime.Parse("05/09/2018 11:45:00 AM").ToUniversalTime();
            r2.Display = r2.ShowingDate.ToLocalTime();
            // r2.StartTime = new TimeSpan(11, 45, 0);
            r2.DayOfWeek = r2.ShowingDate.DayOfWeek;
            r2.Movie = (db.Movies.FirstOrDefault(m => m.Title == "WarGames"));
            db.Showings.AddOrUpdate(r2);
            db.SaveChanges();

            Showing r3 = new Showing();
            r3.Theatre = Theatre.Theatre1;
            r3.ShowingDate = DateTime.Parse("05/09/2018 02:10:00 PM").ToUniversalTime();
            r3.Display = r3.ShowingDate.ToLocalTime();
            // r3.StartTime = new TimeSpan(14, 10, 0);
            r3.DayOfWeek = r3.ShowingDate.DayOfWeek;
            r3.Movie = (db.Movies.FirstOrDefault(m => m.Title == "Office Space"));
            db.Showings.AddOrUpdate(r3);
            db.SaveChanges();

            Showing r4 = new Showing();
            r4.Theatre = Theatre.Theatre1;
            r4.ShowingDate = DateTime.Parse("05/09/2018 04:15:00 PM").ToUniversalTime();
            r4.Display = r4.ShowingDate.ToLocalTime();
            //r4.StartTime = new TimeSpan(16, 15, 0);
            r4.DayOfWeek = r4.ShowingDate.DayOfWeek;
            r4.Movie = (db.Movies.FirstOrDefault(m => m.Title == "Diamonds are Forever"));
            db.Showings.AddOrUpdate(r4);
            db.SaveChanges();

            Showing r5 = new Showing();
            r5.Theatre = Theatre.Theatre1;
            r5.ShowingDate = DateTime.Parse("05/09/2018 06:40:00 PM").ToUniversalTime();
            r5.Display = r5.ShowingDate.ToLocalTime();
            //r5.StartTime = new TimeSpan(18, 40, 0);
            r5.DayOfWeek = r5.ShowingDate.DayOfWeek;
            r5.Movie = (db.Movies.FirstOrDefault(m => m.Title == "West Side Story"));
            db.Showings.AddOrUpdate(r5);
            db.SaveChanges();

            Showing r6 = new Showing();
            r6.Theatre = Theatre.Theatre1;
            r6.ShowingDate = DateTime.Parse("05/09/2018 09:55:00 PM").ToUniversalTime();
            r6.Display = r6.ShowingDate.ToLocalTime();
            //r6.StartTime = new TimeSpan(21, 55, 0);
            r6.DayOfWeek = r6.ShowingDate.DayOfWeek;
            r6.Movie = (db.Movies.FirstOrDefault(m => m.Title == "Psycho"));
            db.Showings.AddOrUpdate(r6);
            db.SaveChanges();

            Showing r7 = new Showing();
            r7.Theatre = Theatre.Theatre2;
            r7.ShowingDate = DateTime.Parse("05/09/2018 09:10:00 AM").ToUniversalTime();
            r7.Display = r7.ShowingDate.ToLocalTime();
            //r7.StartTime = new TimeSpan(9, 10, 0);
            r7.DayOfWeek = r7.ShowingDate.DayOfWeek;
            r7.Movie = (db.Movies.FirstOrDefault(m => m.Title == "Mary Poppins"));
            db.Showings.AddOrUpdate(r7);
            db.SaveChanges();

            Showing r8 = new Showing();
            r8.Theatre = Theatre.Theatre2;
            r8.ShowingDate = DateTime.Parse("05/09/2018 12:00:00 PM").ToUniversalTime();
            r8.Display = r8.ShowingDate.ToLocalTime();
            //r8.StartTime = new TimeSpan(12, 00, 0);
            r8.DayOfWeek = r8.ShowingDate.DayOfWeek;
            r8.Movie = (db.Movies.FirstOrDefault(m => m.Title == "The Muppet Movie"));
            db.Showings.AddOrUpdate(r8);
            db.SaveChanges();

            Showing r9 = new Showing();
            r9.Theatre = Theatre.Theatre2;
            r9.ShowingDate = DateTime.Parse("05/09/2018 02:20:00 PM").ToUniversalTime();
            r9.Display = r9.ShowingDate.ToLocalTime();
            //r9.StartTime = new TimeSpan(14, 20, 0);
            r9.DayOfWeek = r9.ShowingDate.DayOfWeek;
            r9.Movie = (db.Movies.FirstOrDefault(m => m.Title == "The Princess Bride"));
            db.Showings.AddOrUpdate(r9);
            db.SaveChanges();

            Showing r10 = new Showing();
            r10.Theatre = Theatre.Theatre2;
            r10.ShowingDate = DateTime.Parse("05/09/2018 04:30:00 PM").ToUniversalTime();
            r10.Display = r10.ShowingDate.ToLocalTime();
            //r10.StartTime = new TimeSpan(16, 30, 0);
            r10.DayOfWeek = r10.ShowingDate.DayOfWeek;
            r10.Movie = (db.Movies.FirstOrDefault(m => m.Title == "The Lego Movie"));
            db.Showings.AddOrUpdate(r10);
            db.SaveChanges();

            Showing r11 = new Showing();
            r11.Theatre = Theatre.Theatre2;
            r11.ShowingDate = DateTime.Parse("05/09/2018 06:45:00 PM").ToUniversalTime();
            r11.Display = r11.ShowingDate.ToLocalTime();
            //r11.StartTime = new TimeSpan(18, 45, 0);
            r11.DayOfWeek = r11.ShowingDate.DayOfWeek;
            r11.Movie = (db.Movies.FirstOrDefault(m => m.Title == "Finding Nemo"));
            db.Showings.AddOrUpdate(r11);
            db.SaveChanges();

            Showing r12 = new Showing();
            r12.Theatre = Theatre.Theatre2;
            r12.ShowingDate = DateTime.Parse("05/09/2018 08:55:00 PM").ToUniversalTime();
            r12.Display = r12.ShowingDate.ToLocalTime();
            //r12.StartTime = new TimeSpan(20, 55, 0);
            r12.DayOfWeek = r12.ShowingDate.DayOfWeek;
            r12.Movie = (db.Movies.FirstOrDefault(m => m.Title == "Harry Potter and the Goblet of Fire"));
            db.Showings.AddOrUpdate(r12);
            db.SaveChanges();

        }
    }
}