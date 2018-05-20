using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team22project.Models
{
    public class Ticket
    {
        [Key]
        public Int32 TicketID { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Price { get; set; }

        [Required(ErrorMessage = "You must specify a quantity of products")]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        [Display(Name = "Quantity")]
        public Int32 Quantity { get; set; }

        [Display(Name = "Extended Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ExtendedPrice { get; set; }

        public String Seat { get; set; }

        public virtual Showing Showing { get; set; }

        public virtual Transaction Transaction { get; set; }

        public Ticket()
        {
           
        }

    }
}