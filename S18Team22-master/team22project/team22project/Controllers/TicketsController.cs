using System;
using System.Collections;
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
    public class TicketsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Tickets
        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.AllShowings = GetAllShowings();
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,OrderNum,Showing,Seat")] Ticket ticket)
        {
            String UserID = User.Identity.GetUserId();
            List<Ticket> tickets = db.Tickets.Where(r => r.Transaction.AppUser.Id == UserID).ToList();

            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("PurchaseTickets", new { TicketID = ticket.TicketID });
            }

            return View(ticket);
        }

        public ActionResult PurchaseTickets(Int32 TicketID)
        {
            Transaction transaction = new Transaction();

            Ticket t = db.Tickets.Find(TicketID);

            transaction.Tickets.Add(t);

            ViewBag.AllShowings = GetAllShowings();
            return View(t);
        }

        [HttpPost]
        public ActionResult PurchaseTickets(Ticket ticket, int SelectedShowing, AppUser user)
        {
            Showing s = db.Showings.Find(SelectedShowing);

            ticket.Showing = s;


            TimeSpan matineetime = new TimeSpan(12, 0, 0);

            TimeSpan tuesdaydiscount = new TimeSpan(17, 0, 0);

            if (s.DayOfWeek == DayOfWeek.Monday || s.DayOfWeek == DayOfWeek.Tuesday || s.DayOfWeek == DayOfWeek.Wednesday || s.DayOfWeek == DayOfWeek.Thursday)
            {
                if (s.ShowingDate.TimeOfDay < matineetime)
                {
                    ticket.Price = 5;
                }

                else
                {
                    ticket.Price = 10;
                }


            }

            if (s.DayOfWeek == DayOfWeek.Tuesday && s.ShowingDate.TimeOfDay < tuesdaydiscount)
            {
                ticket.Price = 8;
            }

            if (s.DayOfWeek == DayOfWeek.Friday || s.DayOfWeek == DayOfWeek.Saturday || s.DayOfWeek == DayOfWeek.Sunday)
            {
                if (s.DayOfWeek == DayOfWeek.Friday && s.ShowingDate.TimeOfDay < matineetime)
                {
                    ticket.Price = 5;
                }

                else
                {
                    ticket.Price = 12;
                }
            }

            var today = DateTime.Today;
            var age = today.Year - user.Birthday.Year;
            if (age >= 60)
            {
                ticket.Price = ticket.Price - 2;
            }

            TimeSpan difference = ticket.Transaction.TransactionDate - s.ShowingDate;
            if (difference.TotalHours > 48)
            {
                ticket.Price = ticket.Price - 1;
            }

            return View(ticket);


        }


        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,OrderNum,Showing")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public SelectList GetAllShowings()
        {
            List<Showing> allShowings = db.Showings.OrderBy(p => p.ShowingID).ToList();

            SelectList selShowings = new SelectList(allShowings, "ShowingID", "Movie.Title");

            return selShowings;
        }

        public static SelectList EasyAvailableSeats(IList tickets)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
