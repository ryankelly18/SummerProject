using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace team22project.Models
{
    public enum TStatus { Pending, Complete, Cancelled};
    public class Transaction
    {
        public const Decimal SALES_TAX = 0.0825m;

        [Key]
        public Int32 TransactionID { get; set; }

        [Display(Name = "Transaction Number")]
        public Int32 TransactionNumber { get; set; }

        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Order Subtotal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderSubtotal { get; set; }

        [Display(Name = "Order Status")]
        public TStatus TStatus { get; set; }

        [Display(Name = "Gift Status")]
        public bool gift { get; set; }

        [Display(Name = "Sales Tax (8.25%)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal SalesTax
        {
            get { return OrderSubtotal * SALES_TAX; }
        }

        [Display(Name = "Order Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderTotal
        {
            get { return OrderSubtotal + SalesTax; }
        }

        public virtual List<Ticket> Tickets { get; set; }
        public virtual List<Discount> Discounts { get; set; }
        public virtual AppUser AppUser { get; set; }

        public Transaction()
        {
            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }
            if (Discounts == null)
            {
                Discounts = new List<Discount>();
            }
        }
    }
}