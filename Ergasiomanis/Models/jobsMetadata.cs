using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class jobsMetadata
    {
        [Display(Name = "Job Id")]
        [Required]
        public short job_id;

        [Display(Name = "Job Description")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Job description must be bewteen 3 and 50 characters!")]
        public string job_desc;

        [Display(Name = "Minimum level")]
        [Required]      
        [Range(10,250, ErrorMessage ="Minimum level must be between 10 and 250")]
        public byte min_lvl;

        [Display(Name = "Maximum level")]
        [Required]    
        [Range(0,250, ErrorMessage ="Maximum level must be less than 250 and greater than Minimum level!")]
        public byte max_lvl;

    }
}