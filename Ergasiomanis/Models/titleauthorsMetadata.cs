using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class titleauthorsMetadata
    {
        [Display(Name = "Author Id")]
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Author's Id must be 11 characters!")]
        public string au_id;

        [Display(Name = "Title Id")]
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Title Id must be 6 characters!")]
        public string title_id;

        [Display(Name = "")]
        public byte au_ord;

    }
}