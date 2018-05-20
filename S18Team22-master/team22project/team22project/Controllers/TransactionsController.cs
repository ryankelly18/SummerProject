using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using team22project.DAL;
using team22project.Models;
using Microsoft.AspNet.Identity;

namespace team22project.Controllers
{
    public class TransactionsController : Controller
    {
        private AppDbContext db = new AppDbContext();



        // GET: Transactions
        public ActionResult Index()
        {
            return View(db.Transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }


        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.AvailableSeats = GetAvailableSeats();
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "TransactionID,TransactionNumber,TransactionDate,gift")] Transaction transaction, string[] SelectedSeats, Ticket ticket, Showing showing)
        {
            //String UserID = User.Identity.GetUserId();
            transaction.TransactionNumber = Utilities.GenerateTransactionNumber.GetNextTransactionNumber();

            transaction.TransactionDate = DateTime.Now;

            //List<Transaction> transactions = db.Transactions.Where(r => r.AppUser.Id == UserID).ToList();

            Showing s = db.Showings.Find(showing.ShowingID);

            String id = User.Identity.GetUserId();
            AppUser appuser = db.Users.Find(id);
            transaction.AppUser = appuser;

            if (ModelState.IsValid)
            {
                ticket.Showing = s;
                db.Transactions.Add(transaction);
                db.SaveChanges();
            }

            foreach (String i in SelectedSeats)
            {

                ticket.Seat = i;

            }

            return RedirectToAction("AddTickets", new { TransactionID = transaction.TransactionID, TicketID = ticket.TicketID });


            //return View(transaction);
        }





        public ActionResult AddTickets(int TransactionID, int TicketID)
        {
            Transaction tr = db.Transactions.Find(TransactionID);

            
            ViewBag.AvailableSeats = GetAvailableSeats();

            return View();
        }



        [HttpPost]
        public ActionResult AddTickets(Ticket td,  String seat, Transaction tr, string[] SelectedSeats, AppUser user)

        {
            

            //List<SelectListItem> AvailableSeats = GetAvailableSeats(td.Seat).ToList();

            //var availableseats = GetAvailableSeats(sh.Tickets).ToList();

            var SeatCount = 0;
            SeatCount = td.Quantity - 1;

            List<Ticket> TicketList = new List<Ticket>();

            foreach (Ticket i in TicketList)
            {
                Ticket t = new Ticket();
                //t.Showing = s;
                t.Transaction = tr;
                t.Seat = seat;
                SeatCount = SeatCount - 1;
                TicketList.Add(t);

                if (ModelState.IsValid)
                {
                    db.Tickets.Add(t);

                }
            }



            var s = td.Showing;

            TimeSpan matineetime = new TimeSpan(12, 0, 0);

            TimeSpan tuesdaydiscount = new TimeSpan(17, 0, 0);

            if (s.DayOfWeek == DayOfWeek.Monday || s.DayOfWeek == DayOfWeek.Tuesday || s.DayOfWeek == DayOfWeek.Wednesday || s.DayOfWeek == DayOfWeek.Thursday)
            {
                if (s.ShowingDate.TimeOfDay < matineetime)
                {

                    td.Price = 5;

                    td.Price = 5;

                }

                else
                {
                    td.Price = 10;

                    td.Price = 10;

                }


            }

            if (s.DayOfWeek == DayOfWeek.Tuesday && s.ShowingDate.TimeOfDay < tuesdaydiscount)
            {
                td.Price = 8;

                td.Price = 8;

            }

            if (s.DayOfWeek == DayOfWeek.Friday || s.DayOfWeek == DayOfWeek.Saturday || s.DayOfWeek == DayOfWeek.Sunday)
            {
                if (s.DayOfWeek == DayOfWeek.Friday && s.ShowingDate.TimeOfDay < matineetime)
                {

                    td.Price = 5;

                }

                else
                {

                    td.Price = 12;

                    td.Price = 12;

                }
            }

            var today = DateTime.Today;

            //var age = today.Year - user.Birthday.Year;
            //if (age >= 60)
            //{
            //    td.ticketPrice = td.ticketPrice - 2;
            //}

            var age = today.Year - user.Birthday.Year;
            if (age >= 60)
            {
                td.Price = td.Price - 2;
            }


            TimeSpan difference = tr.TransactionDate - s.ShowingDate;
            if (difference.TotalHours > 48)
            {

                td.Price = td.Price - 1;
            }

            td.ExtendedPrice = td.Price * td.Quantity;


            if (ModelState.IsValid)
            {
                db.Tickets.Add(td);
                db.SaveChanges();
                return RedirectToAction("Details", "Transactions", new { id = td.Showing });
            }

            // ViewBag.AvailableTickets = GetAvailableTickets();
            return View("Index");
            //return View(ticket);


        }





        //        //        // GET: Transactions/Edit/5
        //        //        public ActionResult Edit(int? id)
        //        //        {
        //        //            if (id == null)
        //        //            {
        //        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        //            }
        //        //            Transaction transaction = db.Transactions.Find(id);
        //        //            if (transaction == null)
        //        //            {
        //        //                return HttpNotFound();
        //        //            }
        //        //            return View(transaction);
        //        //        }

        //        //        // POST: Transactions/Edit/5
        //        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        //        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //        //        [HttpPost]
        //        //        [ValidateAntiForgeryToken]
        //        //        public ActionResult Edit([Bind(Include = "TransactionID,TransactionNumber,TransactionDate,OrderTotal,Tstatus")] Transaction transaction)
        //        //        {
        //        //            if (ModelState.IsValid)
        //        //            {
        //        //                db.Entry(transaction).State = EntityState.Modified;
        //        //                db.SaveChanges();
        //        //                return RedirectToAction("Index");
        //        //            }
        //        //            return View(transaction);
        //        //        }

        //        //        // GET: Transactions/Delete/5
        //        //        public ActionResult Delete(int? id)
        //        //        {
        //        //            if (id == null)
        //        //            {
        //        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        //            }
        //        //            Transaction transaction = db.Transactions.Find(id);
        //        //            if (transaction == null)
        //        //            {
        //        //                return HttpNotFound();
        //        //            }
        //        //            return View(transaction);
        //        //        }

        //        //        // POST: Transactions/Delete/5
        //        //        [HttpPost, ActionName("Delete")]
        //        //        [ValidateAntiForgeryToken]
        //        //        public ActionResult DeleteConfirmed(int id)
        //        //        {
        //        //            Transaction transaction = db.Transactions.Find(id);
        //        //            db.Transactions.Remove(transaction);
        //        //            db.SaveChanges();
        //        //            return RedirectToAction("Index");
        //        //        }

        //        //        public SelectList GetAllShowings()
        //        //        {
        //        //            List<Showing> allShowings = db.Showings.OrderBy(p => p.Movie).ToList();

        //        //            SelectList selShowings = new SelectList(allShowings, "ShowingID", "Movie");

        //        //            return selShowings;
        //        //        }
    
        public static SelectList GetAvailableSeats()
        {
            List<String> AllSeatNames = new List<String>();
            AllSeatNames.Add("A1");
            AllSeatNames.Add("A2");
            AllSeatNames.Add("A3");
            AllSeatNames.Add("A4");
            AllSeatNames.Add("A5");
            AllSeatNames.Add("A6");
            AllSeatNames.Add("A7");
            AllSeatNames.Add("A8");
            AllSeatNames.Add("B1");
            AllSeatNames.Add("B2");
            AllSeatNames.Add("B3");
            AllSeatNames.Add("B4");
            AllSeatNames.Add("B5");
            AllSeatNames.Add("B6");
            AllSeatNames.Add("B7");
            AllSeatNames.Add("B8");
            AllSeatNames.Add("C1");
            AllSeatNames.Add("C2");
            AllSeatNames.Add("C3");
            AllSeatNames.Add("C4");
            AllSeatNames.Add("C5");
            AllSeatNames.Add("C6");
            AllSeatNames.Add("C7");
            AllSeatNames.Add("C8");
            AllSeatNames.Add("D1");
            AllSeatNames.Add("D2");
            AllSeatNames.Add("D3");
            AllSeatNames.Add("D4");
            AllSeatNames.Add("D5");
            AllSeatNames.Add("D6");
            AllSeatNames.Add("D7");
            AllSeatNames.Add("D8");

            SelectList slAvailableSeats = new SelectList(AllSeatNames);

            return slAvailableSeats;
        }
         
        public static SelectList GetAvailableSeats(List<Ticket> tickets)
        {
            List<String> TakenSeatNames = new List<String>();
            foreach (Ticket t in tickets)
            {
                TakenSeatNames.Add(t.Seat);
            }

            List<String> AllSeatNames = new List<String>();
            AllSeatNames.Add("A1");
            AllSeatNames.Add("A2");
            AllSeatNames.Add("A3");
            AllSeatNames.Add("A4");
            AllSeatNames.Add("A5");
            AllSeatNames.Add("A6");
            AllSeatNames.Add("A7");
            AllSeatNames.Add("A8");
            AllSeatNames.Add("B1");
            AllSeatNames.Add("B2");
            AllSeatNames.Add("B3");
            AllSeatNames.Add("B4");
            AllSeatNames.Add("B5");
            AllSeatNames.Add("B6");
            AllSeatNames.Add("B7");
            AllSeatNames.Add("B8");
            AllSeatNames.Add("C1");
            AllSeatNames.Add("C2");
            AllSeatNames.Add("C3");
            AllSeatNames.Add("C4");
            AllSeatNames.Add("C5");
            AllSeatNames.Add("C6");
            AllSeatNames.Add("C7");
            AllSeatNames.Add("C8");
            AllSeatNames.Add("D1");
            AllSeatNames.Add("D2");
            AllSeatNames.Add("D3");
            AllSeatNames.Add("D4");
            AllSeatNames.Add("D5");
            AllSeatNames.Add("D6");
            AllSeatNames.Add("D7");
            AllSeatNames.Add("D8");


            List<String> AvailableSeats = AllSeatNames.Except(TakenSeatNames).ToList();


            

            SelectList slAvailableSeats = new SelectList(AvailableSeats);

            return slAvailableSeats;
        }
    }
}





//// GET: Transactions/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Transaction transaction = db.Transactions.Find(id);
//            if (transaction == null)
//            {
//                return HttpNotFound();
//            }
//            return View(transaction);
//        }

//        // POST: Transactions/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "TransactionID,TransactionNumber,TransactionDate,OrderTotal,Tstatus")] Transaction transaction)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(transaction).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(transaction);
//        }

//        // GET: Transactions/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Transaction transaction = db.Transactions.Find(id);
//            if (transaction == null)
//            {
//                return HttpNotFound();
//            }
//            return View(transaction);
//        }

//        // POST: Transactions/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Transaction transaction = db.Transactions.Find(id);
//            db.Transactions.Remove(transaction);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        public SelectList GetAllShowings()
//        {
//            List<Showing> allShowings = db.Showings.OrderBy(p => p.Movie).ToList();

//            SelectList selShowings = new SelectList(allShowings, "ShowingID", "Movie");

//            return selShowings;
//        }




//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}

