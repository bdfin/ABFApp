using ABF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace ABF
{
    [Authorize(Roles = "Admin")]
    public class InitAdminController : Controller
    {        
        public ApplicationDbContext db;
        public DropCreateDatabaseIfModelChanges<DbContext> dbInit;

        public InitAdminController()
        {
            db = new ApplicationDbContext();
            dbInit = new DropCreateDatabaseIfModelChanges<DbContext>();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string adminUserEmail = "admin@example.com", string adminPassword = "Admin@123456")
        {
            bool init = Init(db, adminUserEmail, adminPassword);
            if (init)
            {
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public bool Init(ApplicationDbContext db, string adminUserEmail, string adminPassword)
        {
            try
            {
                dbInit.InitializeDatabase(db);

                var userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                string name = adminUserEmail;
                string password = adminPassword;

                var user = userManager.FindByName(name);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = name, Email = name };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                return true;
            }

            catch(Exception ex)
            {
                var ex1 = ex;
                return false;
            }
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}