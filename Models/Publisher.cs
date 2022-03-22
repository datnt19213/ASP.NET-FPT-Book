using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBookProject.Models
{
    public class Publisher
    {
        [Key]

        [Required(ErrorMessage = "Pulisher ID can not be empty")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Publisher Name can not be empty")]
        public string PublisherName { get; set; }

        [Required(ErrorMessage = "Description can not be empty")]
        public string Description { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}