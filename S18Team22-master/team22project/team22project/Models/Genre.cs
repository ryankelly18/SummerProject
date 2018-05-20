using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team22project.Models
{
    public class Genre
    {
        [Key]
        public Int32 GenreID { get; set; }

        [Display(Name = "Genre")]
        public String GenreName { get; set; }

        public virtual List<Movie> Movies { get; set; }
    }
}