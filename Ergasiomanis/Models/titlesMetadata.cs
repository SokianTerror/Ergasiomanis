using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class titlesMetadata
    {
        [Display(Name = "Title Id")]
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Title id must be 6 characters!")]
        public string title_id;

        [Display(Name = "Title")]
        [Required]
        [StringLength(80, ErrorMessage = "The title of the book should not exceed 80 characters!")]
        public string title;

        [Display(Name = "Type")]
        [Required]
        [StringLength(12, ErrorMessage = "Type should not exceed 6 characters!")]
        public string type;

        [Display(Name = "Publisher Id")]
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Publisher's id must be 4 characters!")]
        public string pub_id;

        [Display(Name = "Price")]
        public decimal price;

        [Display(Name = "Advance")]
        public decimal advance;

        [Display(Name = "Royalty")]
        public int royalty;

        [Display(Name = "Times Sold")]
        public int ytd_sales;

        [Display(Name = "Notes")]
        [StringLength(200, ErrorMessage = "The number of characters in the notes should not be more than 200!")]
        public string notes;

        [Display(Name = "Release Date")]
        [DataType(DataType.DateTime)]
        public DateTime pubdate;

    }
}