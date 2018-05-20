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

namespace team22project.Controllers
{
    public enum SortOrder { greater, lesser }

    public class MoviesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        //// GET: Movies
        //public ActionResult Index()
        //{
        //    return View(db.Movies.ToList());
        //}

        public ActionResult Index(String SearchString)
        {
            List<Movie> SelectedMovies = new List<Movie>();

            //create query

            var query = from r in db.Movies

                        select r;

            //check to see if they selected something

            if (SearchString != null)
            {
                //if they selected a search string, limit results to only repos that meet the criteria
                query = query.Where(r => r.Title.Contains(SearchString) || r.Overview.Contains(SearchString) || r.Actors.Contains(SearchString) || r.Tagline.Contains(SearchString));
            }

            //execute query
            SelectedMovies = query.ToList();

            //order list
            SelectedMovies.OrderByDescending(r => r.ReleaseDate);

            ViewBag.TotalMovies = db.Movies.Count();

            ViewBag.SelectedMovies = SelectedMovies.Count();

            //send list to view
            return View(SelectedMovies);

        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //GET: Showings/Details/5
        public ActionResult Showings(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var newid = from m in db.Movies
                        select m;
            newid = newid.Where(m => m.Showings.Exists(x => x.Movie.MovieID == m.MovieID)/*(x => x.Movie.MovieID == m.MovieID)*/);
            Showing show = db.Showings.Find(newid);

            return View(show);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.AllGenres = GetGenres();
            return View();
        }

        //POST: Movies/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID,Title,Overview,Tagline,ReleaseDate,MPAARating,RunTime,Actor")] Movie movie, int[] SelectedGenre)
        {
            movie.MovieNumber = Utilities.GenerateNextMovieNumber.GetNextMovieNumber();

            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieID,Title,Tagline,ReleaseDate,MPAARating,RunTime")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public MultiSelectList GetAllGenres()

        {

            List<Genre> allGens = db.Genres.OrderBy(d => d.GenreName).ToList();

            MultiSelectList selGens = new MultiSelectList(allGens, "GenreID", "GenreName");

            return selGens;

        }

        public MultiSelectList GetAllGenres(Movie movie)

        {

            List<Genre> allGenres = db.Genres.OrderBy(d => d.GenreName).ToList();

            //convert list of selected departments to ints
            List<Int32> SelectedGenres = new List<Int32>();

            //loop through the course's departments and add the department id
            foreach (Genre gen in movie.Genres)

            {
                SelectedGenres.Add(gen.GenreID);
            }

            //create the multiselect list
            MultiSelectList selGens = new MultiSelectList(allGenres, "GenreID", "GenreName", SelectedGenres);

            //return the multiselect list
            return selGens;

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetGenres();
            return View();
        }

        public ActionResult DisplaySearchResults(bool ShowingOnly, String strTitle, String strTagline, Int32? intSelectedYear, MPAARating MPAArating, String strActors, Int32? SelectedGenre, DateTime? datSchedule)
        {
            //create query
            var query = from m in db.Movies
                        select m;
            if (ShowingOnly == true)
            {
                query = query.Where(m => m.Showings.Any(x => x.Movie.MovieID == m.MovieID));
            }

            //check to see if they selected something
            if (strTitle != null)
            {
                //if they selected a search string, limit results to only repos that meet the criteria
                query = query.Where(m => m.Title.Contains(strTitle));
            }

            if (strTagline != null)
            {
                query = query.Where(m => m.Tagline.Contains(strTagline));
            }

            if (intSelectedYear != null)
            {
                query = query.Where(m => m.ReleaseDate.Year == intSelectedYear);
            }
            if (MPAArating != MPAARating.Any)
            {
                if (MPAArating == MPAARating.G)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.G);
                }
                if (MPAArating == MPAARating.PG)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.PG);
                }
                if (MPAArating == MPAARating.PG13)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.PG13);
                }
                if (MPAArating == MPAARating.R)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.R);
                }
                if (MPAArating == MPAARating.Unrated)
                {
                    query = query.Where(m => m.MPAARating == MPAARating.Unrated);
                }
            }
            if (strActors != null)
            {
                query = query.Where(m => m.Actors.Contains(strActors));
            }

            if (SelectedGenre != null)
            {
                query = query.Where(m => m.Genres.Any(x => x.GenreID == SelectedGenre));
            }
            if (datSchedule != null)
            {
                query = query.Where(m => m.Showings.Any(x => x.Movie.MovieID == m.MovieID && x.ShowingDate == datSchedule));

            }


            //execute query
            List<Movie> MoviesToDisplay = query.ToList();
            ViewBag.TotalMovies = db.Movies.Count();
            ViewBag.SelectedMovies = MoviesToDisplay.Count();
            //order lsit
            //MoviesToDisplay.OrderByDescending(r => r.StarCount);
            
            if (MoviesToDisplay.Count == 0)
            {
                return View("NoMovies");
            }

            //send list to view
            return View("Index", MoviesToDisplay);

        }


        public MultiSelectList GetGenres()
        {
            List<Genre> Genres = db.Genres.ToList();

            Genre SelectNone = new Models.Genre();
            Genres.Add(SelectNone);

            MultiSelectList AllGenres = new MultiSelectList(Genres.OrderBy(m => m.GenreID), "GenreID", "GenreName");

            return AllGenres;
        }
    }
}


