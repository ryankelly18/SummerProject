using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using team22project.Models;
using team22project.DAL;
using System.Data.Entity;

namespace team22project.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Home
        public ActionResult Index()
        {
            //create query
            var query = from s in db.Showings
                        select s;
            //query = query.Where(s => s.ShowingDate.Date == DateTime.Today.Date);

            //return View();
            return View(query.ToList());
        }
    }
}