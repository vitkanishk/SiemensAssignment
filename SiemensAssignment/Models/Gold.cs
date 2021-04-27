using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiemensAssignment.Models
{
    public class Gold
    {
        [Required(ErrorMessage ="Please enter Price")]
        [Range(1,99999)]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter Weight")]
        [Range(1, 99999)]
        public double Weight { get; set; }

        [Editable(false)]
        public double? TotalPrice { get; set; }

        [ReadOnly(true)]
        public double? Discount { get; set; }
    }
}