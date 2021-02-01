using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class pub_infoMetadata
    {
        [Display(Name = "Publisher Id")]
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Publisher Id must be 4 characters!")]
        public string pub_id;

       // [Display(Name = "Logo")]
      //  public byte[] logo;

        [Display(Name = "Publisher Info")]
        public string pr_info;
    }
}