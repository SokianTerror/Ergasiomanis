using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class authorsMetadata
    {
        [Display(Name = "Author id")]
        [Required]
        [StringLength(11, ErrorMessage = "Author id should not be more than 11 characters!")]
        public string au_id;

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(40, ErrorMessage = "Last Name can not be more than 40 characters!")]
        public string au_lname;

        [Display(Name = "First Name")]
        [Required]
        [StringLength(20, ErrorMessage = "First Name can not be more than 20 characters!")]
        public string au_fname;

        [Display(Name = "Phone")]
        [Required]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Incorrect phone number!")]
        public string phone;

        [Display(Name = "Address")]
        [StringLength(40, ErrorMessage = "Address should not exceed 40 characters!")]
        public string address;

        [Display(Name = "City")]
        [StringLength(20, ErrorMessage = "City name should not exceed 20 characters!")]
        public string city;

        [Display(Name = "State")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State name must be 2 characters.")]
        public string state;

        [Display(Name = "Zip")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Zip must be 5 numbers.")]
        public string zip;

        [Display(Name = "Contract")]
        public bool contract;
    }
}