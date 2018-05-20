using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace team22project.Models
{
    public class Discount
    {
        [Key]
        public Int32 DiscountID { get; set; }

        [Display(Name = "Discount Amount")]
        public Decimal DiscountAmount { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}