using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTBookProject.Models
{
    public class BookCart
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Img { get; set; }
        public int quantity1 { get; set; }
        public int Price { get; set; }
    }
}