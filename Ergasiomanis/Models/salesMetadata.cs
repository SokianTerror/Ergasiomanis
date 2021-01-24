using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ergasiomanis.Models
{
    public class salesMetadata
    {
        [Display(Name = "Store Id")]
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Store id must be 4 characters!")]
        public string stor_id;

        [Display(Name = "Order Number")]
        [Required]
        [StringLength(20, ErrorMessage = "Order number should not exceed 20 characters!")]
        public string ord_num;

        [Display(Name = "Order Date")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ord_date;

        [Display(Name = "Quantity")]
        [Required]
        public short qty;

        [Display(Name = "Payment Terms")]
        [Required]
        [StringLength(12, ErrorMessage = "Payment terms should not exceed 12 characters!")]
        public string payterms;

        [Display(Name = "Title Id")]
        [Required]
        [StringLength(6, ErrorMessage = "Title id should not exceed 6 characters")]
        public string title_id;
    }
}