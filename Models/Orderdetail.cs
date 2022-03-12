using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBookProject.Models
{
    public class Orderdetail
    {
        [Key]

        //[Required]
        //public int OrderDetailId { get; set; }

        [Required]
        public int BookId { get; set; }
        [Key]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Please in input positive number")]
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }
    }
}