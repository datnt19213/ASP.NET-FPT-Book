using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBookProject.Models
{
    public class Author
    {
        [Key]

        [Required(ErrorMessage = "Author ID can not be empty")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Author Name can not be empty")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Description can not be empty")]
        public string Description { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}