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

        [Display(Name = "Minit")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Minit must be 1 character!")]
        public string minit;

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(30, ErrorMessage = "Last Name should not exceed 30 characters!")]
        public string lname;

        [Display(Name = "Job Id")]
        [Required]
        public short job_id;

        [Display(Name = "Job Level")]
        public byte job_lvl;

        [Display(Name = "Publisher Id")]
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Pushlisher Id must be 4 characters!")]
        public string pub_id;

        [Display(Name = "Hire Date")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime hire_date;
    }
}