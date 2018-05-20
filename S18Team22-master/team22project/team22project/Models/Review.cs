using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team22project.Models
{
    public class Review
    {
        public Int32 ReviewID { get; set; }

        [Display(Name = "Stars")]
        [Range(1,5)]
        public decimal Stars { get; set; }

        [Display(Name = "Description")]
        [StringLength(100, ErrorMessage = "Review cannot exceed 100 characters")]
        public String Description { get; set; }

        public int UpVoteCount { get; private set; }
        public int DownVoteCount { get; private set; }

        // C#6 expression bodied member
        public int Score => UpVoteCount - DownVoteCount;

        public void UpVote()
        {
            UpVoteCount++;
        }

        public void DownVote()
        {
            DownVoteCount++;
        }

        public virtual Movie Movie { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}