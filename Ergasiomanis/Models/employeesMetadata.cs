using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class employeesMetadata
    {
        [Display(Name = "Employer Id")]
        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Employer id must be 9 characters!")]
        public string emp_id;

        [Display(Name = "First Name")]
        [Required]
        [StringLength(20, ErrorMessage = "First Name should not exceed 20 characters!")]
        public string fname;

        public string minit;

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(30, ErrorMessage = "Last Name should not exceed 30 characters!")]
        public string lname;

        [Display(Name = "Job Id")]
        [Required]
        public short job_id;
    }
}