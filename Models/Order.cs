using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBookProject.Models
{
    public class Order
    {
        [Key]

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int TotalPrice { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Address delivery can not be empty")]
        public string Addressdilivery { get; set; }


        public virtual Account Account { get; set; }
        public ICollection<Orderdetail> Orderdetail { get; set; }
    }
}