using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class discountsMetadata
    {
        [Display(Name = "Discount Type")]
        [Required]
        [StringLength()]
        public string discounttype;
    }
}