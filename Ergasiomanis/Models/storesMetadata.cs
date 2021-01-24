using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class storesMetadata
    {
        [Display(Name = "Store id")]
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Store id must be 4 characters!")]
        public string stor_id;

        [Display(Name = "Store Name")]
        [StringLength(40, ErrorMessage = "Store name should not exceed 40 characters!")]
        public string stor_name;

        [Display(Name = "Store Address")]
        [StringLength(40, ErrorMessage = "Store address should not exceed 40 characters!")]
        public string stor_address;

        [Display(Name = "City")]
        [StringLength(20, ErrorMessage = "City name should not exceed 20 characters!")]
        public string city;

        [Display(Name = "State")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State name must be 2 characters!")]
        public string state;

        [Display(Name = "Zip")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Zip must be 5 characters!")]
        public string zip;
    }
}