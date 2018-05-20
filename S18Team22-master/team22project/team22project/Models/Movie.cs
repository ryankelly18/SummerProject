using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team22project.Models
{
    public enum MPAARating { G, PG, PG13, R, NC17, Unrated, Any }

    public class Movie
    {
        [Key]
        public Int32 MovieID { get; set; }

        [Display(Name = "Movie Number")]
        public Int32 MovieNumber {get; set;}

        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        public String Title { get; set; }

        [Display(Name = "Overview")]
        public String Overview { get; set; }

        [Display(Name = "Tagline")]
        public String Tagline { get; set; }

        [Required(ErrorMessage = "Release Date is required")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Revenue")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public Decimal Revenue { get; set; }

        [Required(ErrorMessage = "MPAA Rating is required")]
        [Display(Name = "MPAA Rating")]
        public MPAARating MPAARating { get; set; }

        [Required(ErrorMessage = "Run time is required")]
        [Display(Name = "Run Time")]
        public Int32 RunTime { get; set; }

        public String Actors { get; set; }

        public Decimal ReviewAvg
        {
            get { if (Reviews.Count == 0) { return 0; } else { return Reviews.Average(x => x.Stars); } }

        }

        public virtual List<Genre> Genres { get; set; }
        public virtual List<Review> Reviews { get; set; }
        public virtual List<Showing> Showings { get; set; }

        public Movie()
        {
            if (Genres == null)
            {
                Genres = new List<Genre>();
            }
            if (Showings == null)
            {
                Showings = new List<Showing>();
            }
            if (Reviews == null)
            {
                Reviews = new List<Review>();
            }

        }
    }
}