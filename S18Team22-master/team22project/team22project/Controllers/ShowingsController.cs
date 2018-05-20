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

namespace team22project.Controllers
{
    public class ShowingsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Showings
        public ActionResult Index()
        {
            return View(db.Showings.ToList());
        }

        // GET: Showings/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Showings.Find(id);
            if (showing == null)
            {
                return HttpNotFound();
            }
            //ViewBag.GetAllSeats = AvailableSeats();
            return View(showing);
        }

        public SelectList GetAllMovies()
        {
            List<Movie> allMovies = db.Movies.OrderBy(p => p.Title).ToList();

            SelectList selMovies = new SelectList(allMovies, "MovieID", "Title");

            return selMovies;
        }

        // GET: Showings/Create
        public ActionResult Create()
        {
            ViewBag.AllMovies = GetAllMovies();
            return View();
        }

        // POST: Showings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShowingID,ShowingDate,StartTime,Theatre,SpecialEvent")] Showing showing, int SelectedMovie)
        {
            
            if (ModelState.IsValid)
            {
                db.Showings.Add(showing);
                Movie m = db.Movies.Find(SelectedMovie);
                showing.Movie = m;
                showing.DayOfWeek = showing.ShowingDate.DayOfWeek;
                showing.Display = showing.ShowingDate.ToLocalTime();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(showing);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddToOrder(Transaction tran, Showing show, string[] AvailableSeats)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        db.Showings.Add(showing);
        //        Movie m = db.Movies.Find(SelectedMovie);
        //        showing.Movie = m;
        //        showing.DayOfWeek = showing.ShowingDate.DayOfWeek;
        //        showing.Display = showing.ShowingDate.ToLocalTime();
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(showing);
        //}

        // GET: Showings/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Showings.Find(id);
            if (showing == null)
            {
                return HttpNotFound();
            }
            return View(showing);
        }

        // POST: Showings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShowingID,Movie,ShowingDate,TimeofWeek,Theatre,SpecialEvent")] Showing showing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(showing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(showing);
        }

        // GET: Showings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Showings.Find(id);
            if (showing == null)
            {
                return HttpNotFound();
            }
            return View(showing);
        }

        // POST: Showings/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Showing showing = db.Showings.Find(id);
            db.Showings.Remove(showing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public static SelectList AvailableSeats(List<Ticket> Tickets)
        {
            List<String> TakenSeatNames = new List<String>();
            foreach (Ticket t in Tickets)
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

        //public MultiSelectList GetAllSeats()
        //{
        //    List<Seat> allSeats = db.Seats.OrderBy(d => d.SeatName).ToList();

        //    MultiSelectList selSeats = new MultiSelectList(allSeats, "SeatID", "SeatName");

        //    return selSeats;
        //}


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
