using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using team22project.DAL;


namespace team22project.Utilities

{

    public static class GetSeats

    {

        public static Int32 GetNextMovieNumber()

        {

            //we need a db context to connect to the database

            AppDbContext db = new AppDbContext();

            Int32 intMaxCustomerNumber; //the current maximum course number
            Int32 intNextCustomerNumber; //the course number for the next class

            if (db.Users.Count() == 0) //there are no registrations in the database yet

            {
                intMaxCustomerNumber = 5000; //registration numbers start at 101
            }

            else

            {
                intMaxCustomerNumber = db.Users.Max(c => c.CustomerNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextCustomerNumber = intMaxCustomerNumber + 1;

            //return the value
            return intNextCustomerNumber;

        }



    }

}