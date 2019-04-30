using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class Users_in_Role_ViewModel
    {
        [Display(Name = "user ID")]
        public string UserId { get; set; }

        [Display(Name = "UserName")]
        public string Username { get; set; }

        [Display(Name = "Email Addres")]
        public string Email { get; set; }

        [Display(Name = "Roles")]
        public string Role { get; set; }

        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Display(Name = "PostCode")]
        public string PostCode { get; set; }
    }
}