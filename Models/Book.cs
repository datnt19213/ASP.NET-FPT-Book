using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBookProject.Models
{
    public class Book
    {
        [Key]

        [Required(ErrorMessage = "Book ID can not be empty")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Book Name can not be empty")]
        public string BookName { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string Img { get; set; }

        [Required(ErrorMessage = "Quantity can not be empty")]
        [Range(0, 1000, ErrorMessage = "Please in input positive number")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price can not be empty")]
        [Range(0, 1000, ErrorMessage = "Please in input positive number")]
        public int Price { get; set; }

        [Required(ErrorMessage = " Category ID can not be empty")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Author ID can not be empty")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Publisher ID can not be empty")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Book's desciption can not be empty")]
        public string Description { get; set; }
        public ICollection<Orderdetail> Orderdetail { get; set; }
        public virtual Category Category { get; set; }
        public virtual Author Author { get; set; }
        public virtual Publisher Publisher { get; set; }

    }
}