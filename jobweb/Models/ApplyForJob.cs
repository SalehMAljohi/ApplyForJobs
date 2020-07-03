using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace jobweb.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        [DisplayName("الرسالة")]
        public string  Message { get; set; }
        [DisplayName("توقيت التقدم")]
        public DateTime  ApplyDate { get; set;}
        public int JobId { get; set; }
        public string  UserId { get; set; }
        public virtual  Job jobs { get; set; }
        public virtual  ApplicationUser user { get; set; }
    }
}