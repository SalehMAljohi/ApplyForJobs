using jobweb.Controllers;
using jobweb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
         ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult GetJobByUser()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(jobs.ToList());
        }
        [Authorize]
        public ActionResult DetailsOfJob(int Id)
        {
            var job = db.ApplyForJobs.Find(Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        // GET: /Edit
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs .Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST:Edit
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: /Delete
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: /Delete
        [HttpPost]
        public ActionResult Delete(ApplyForJob  job )
        {
            var myjob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(myjob);
            db.SaveChanges();
            return RedirectToAction("GetJobByUser");

        }
        //publishers to knows the people submited for their jobs
        [Authorize]
        public ActionResult GetJobByPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = from ap in db.ApplyForJobs
                       join job in db.Jobs            //
                       on ap.JobId equals job.Id
                       where job.user.Id==UserId 
                       select ap;
            var grouped = from j in jobs
                          group j by j.jobs.JobName  
                        into gr
                          select new JobsViewModel
                          {
                              JobName = gr.Key ,
                               Items = gr
                          };
            return View(grouped.ToList());
        }

    public ActionResult Details(int JobId)
        {
            var  job = db.Jobs.Find(JobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }
       
        [Authorize]
        //GET METHOD
        public ActionResult Apply()
        {
            
            return View();
        }
        //POST METHOD
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var JobId = (int)Session["JobId"];
            var UserId = User.Identity.GetUserId();
            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if(check.Count<1)
            {
                var job = new ApplyForJob();
                job.JobId = JobId;
                job.UserId = UserId;
                job.ApplyDate = DateTime.Now;
                job.Message = Message;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.result = "تمت الاضافة بنجاح!"; 
            }else
            {
                ViewBag.result = "لقد تقدمت على هذه الوظيفة مسبقا!";
            }

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }
       
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            //تفاصيل الرسالة المرسالة 
            var mail = new MailMessage();
            var LogINInfo = new NetworkCredential("salehaljohi33@gmail.com","712750084");
            mail.From = new MailAddress(contact.EMail);
            mail.To.Add(new MailAddress("salehaljohi33@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            var body = "<h3 class='text-success'>اسم المرسل:" + contact.Name + "</h3><br>" +
                      "<h3 class='text-success'>حساب المرسل:" + contact.EMail + "</h3><br>" +
                      "<h3 class='text-success'>الموضوع :" + contact.Subject + "</h3><br>" +
                      "<b><h3 class='text-success'>الرسالة:" + contact.Message + "</h3></b>"; 
            mail.Body = body;
            //هذا لتحدد انها الرالة اتية الى gmail
            var smtpClient = new SmtpClient("smtp.gmail.com",587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = LogINInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search( string searchName)
        {
            var result = db.Jobs.Where(a => a.JobName.Contains(searchName)
             || a.JobContent.Contains(searchName)
             || a.Category.CategoryName.Contains(searchName)
             || a.Category.categoryDescription.Contains(searchName)).ToList();

            return View(result);
        }
    }
}