using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using team22project.Models;

namespace team22project.Models
{
    public enum Theatre { Theatre1, Theatre2}
   

    public class Showing
    {
        [Display(Name = "Showing ID")]
        public Int32 ShowingID { get; set; }


        [Display(Name = "Showing Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ShowingDate { get; set; }


        public DateTime Display { get; set; }
        //public DateTime StartTime { get; set; }

        [Display(Name = "Day of Week")]
        public DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "Theatre")]
        public Theatre Theatre { get; set; }

        public bool SpecialEvent { get; set; }

        public virtual Movie Movie { get; set; }

        public List<Ticket> Tickets { get; set; }

        public Showing()
        {
            if (Tickets == null)
            {
               this.Tickets = new List<Ticket>();
                
                //Tickets.Add(new Ticket("A1"));
                //Tickets.Add(new Ticket("A2"));
                //Tickets.Add("A3");
                //Tickets.Add("A4");
                //Tickets.Add("A5");
                //Tickets.Add("A6");
                //Tickets.Add("A7");
                //Tickets.Add("A8");
                //Tickets.Add("B1");
                //Tickets.Add("B2");
                //Tickets.Add("B3");
                //Tickets.Add("B4");
                //Tickets.Add("B5");
                //Tickets.Add("B6");
                //Tickets.Add("B7");
                //Tickets.Add("B8");
                //Tickets.Add("C1");
                //Tickets.Add("C2");
                //Tickets.Add("C3");
                //Tickets.Add("C4");
                //Tickets.Add("C5");
                //Tickets.Add("C6");
                //Tickets.Add("C7");
                //Tickets.Add("C8");
                //Tickets.Add("D1");
                //Tickets.Add("D2");
                //Tickets.Add("D3");
                //Tickets.Add("D4");
                //Tickets.Add("D5");
                //Tickets.Add("D6");
                //Tickets.Add("D7");
                //Tickets.Add("D8");

            }
        }
        

}
}