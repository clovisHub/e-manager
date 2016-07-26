using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace BeesApp.Models
{
    public class EmailFormModels
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }

        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public string Texto { get; set; }
    }
}