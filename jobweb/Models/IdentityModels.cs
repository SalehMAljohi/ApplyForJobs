using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using jobweb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser//هذا الكلاس الذي ينشاء الجداول
    {
       public string UserType { get; set; }//هنا اضافنا كولمن جديد في جدول IdentityUser table AspNetUsers
        public virtual  ICollection<Job> jobs { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }// الذهب الى console management package 
     // اولا فعلنا المقريشن enable-Migration
     //اضفنا add-migration AddColumn
     //update-database بعد ما نعرف انه UserType ما انضاف الى قاعده البيانات

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<jobweb.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<jobweb.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<jobweb.Models.ApplyForJob> ApplyForJobs { get; set; }

        //هذا من اجل خطاء يحدث في MIGRATINاثناء التشغيلpublic System.Data.Entity.DbSet<WebApplication1.Models.EditProfileViewModel> EditProfileViewModels { get; set; }
    }
}