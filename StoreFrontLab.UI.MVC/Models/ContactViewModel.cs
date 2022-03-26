using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StoreFrontLab.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage ="Your name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Your Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Subject is required")]
        public string Subject { get; set; }

        [UIHint("MultilineText")]
        [Required(ErrorMessage ="A message is required")]
        public string Message { get; set; }
    }
}