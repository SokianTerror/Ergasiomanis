using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class publishersMetadata
    {
        [Display(Name = "Publisher Id")]
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Publisher id must be 4 characters!")]
        public string pub_id;

        [Display(Name = "Publisher Name")]
        [StringLength(40, ErrorMessage = "Publisher name should not exceed 40 characters!")]
        public string pub_name;

        [Display(Name = "City")]
        [StringLength(20, ErrorMessage = "City should not exceed 20 characters!")]
        public string city;

        [Display(Name = "State")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2!")]
        public string state;

        [Display(Name = "Country")]
        [StringLength(30, ErrorMessage = "Country should not exceed ")]
        public string country;
    }
}