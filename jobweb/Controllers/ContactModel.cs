using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobweb.Controllers
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string  EMail { get; set; }
        [Required]
        public string  Subject { get; set; }
        [Required]
        [AllowHtml]
        public string  Message { get; set; }
    }
}