using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using team22project.DAL;

namespace team22project.Utilities
{
    public class GenerateTransactionNumber
    {
        public static Int32 GetNextTransactionNumber()
        {
            AppDbContext db = new AppDbContext();

            Int32 intMaxTransactionNumber;
            Int32 intNextTransactionNumber;

            if (db.Transactions.Count() == 0)
            {
                intMaxTransactionNumber = 10000;
            }
            else
            {
                intMaxTransactionNumber = db.Transactions.Max(o => o.TransactionNumber);
            }

            intNextTransactionNumber = intMaxTransactionNumber + 1;

            return intNextTransactionNumber;
        }

    }
}