using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jobweb.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = " نوع الوظيفة")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "وصف النوع")]
        public string categoryDescription { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}