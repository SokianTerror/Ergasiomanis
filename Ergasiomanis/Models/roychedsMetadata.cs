using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class roychedsMetadata
    {
        [Display(Name = "Title Id")]
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Title id must be 6 characters!")]
        public string title_id;

        [Display(Name = "Low Range")]
        public int lorange;

        [Display(Name = "High Range")]
        public int hirange;

        [Display(Name = "Royalty")]
        public int royalty;
    }
}