using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBookProject.Models
{
    public class Account
    {
        [Required(ErrorMessage = "FullName can not be empty")]
        public string FullName { get; set; }

        [Key]
        [Required(ErrorMessage = "UserName can not be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PassWord can not be empty")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm PassWord can not be empty")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Both Password and Confirm Password must be Same")]
        public string ConfirmPass { get; set; }

        [Required(ErrorMessage = "Phone number can not be empty")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address can not be empty")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email can not be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public int StatusCode { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}