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
        [StringLength(40, ErrorMessage = "Discount Type should not exceed 40 characters!")]
        public string discounttype;

        [Display(Name = "Store id")]
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Store id should be 4 characters!")]
        public string stor_id;

        [Display(Name = "Low quantity")]
        [Required]
        public string lowqty;

        [Display(Name = "High quantity")]
        [Required]
        public string highqty;

        [Display(Name = "Discount")]
        [Required]
        public decimal discount;
    }
}