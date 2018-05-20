
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using team22project.DAL;


namespace team22project.Utilities

{

    public static class GenerateNextMovieNumber

    {

        public static Int32 GetNextMovieNumber()

        {

            //we need a db context to connect to the database

            AppDbContext db = new AppDbContext();

            Int32 intMaxMovieNumber; //the current maximum course number
            Int32 intNextMovieNumber; //the course number for the next class

            if (db.Movies.Count() == 0) //there are no registrations in the database yet

            {
                intMaxMovieNumber = 3000; //registration numbers start at 101
            }

            else

            {
                intMaxMovieNumber = db.Movies.Max(c => c.MovieNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextMovieNumber = intMaxMovieNumber + 1;

            //return the value
            return intNextMovieNumber;

        }



    }

}