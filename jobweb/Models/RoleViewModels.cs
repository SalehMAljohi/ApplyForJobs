using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jobweb.Models
{
    public class RoleViewModels
    {
        public string  Id { get; set;}
        [Required]
        [DisplayName("اسم القاعدة") ]
        public string  Name { get; set; }
    }
}