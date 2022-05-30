using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Models;
using CourseWork.Data;

namespace CourseWork.Controllers
{
    public class AuthenticationController : Controller
    {
        public static int UserKey;

        private AppDbContext appDbContext;

        public AuthenticationController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("Authentication/SignUp")]
        public ViewResult SignUp()
        {
            return View("~/Views/Home/SignUp/SignUp.cshtml");
        }

        [HttpPost]
        [Route("Authentication/SignUp")]
        public ViewResult SignUp(ModelUser r)
        {
            if (ModelState.IsValid && r.RoleBuilder == r.PasswordBuilder)
            {
                var tm = appDbContext.Users.Where(x => x.Login == r.LoginBuilder).FirstOrDefault();
                if (tm != null)
                    return View("Views/Home/SignUp/Error.cshtml");
                else
                {
                    var tmp =new User();
                   // var q = appDbContext.Roles.Where(x => x.Name == "Customer").FirstOrDefault();
                    r.CopyData(tmp,tmp.RoleId);
                    appDbContext.Users.Add(tmp);
                    appDbContext.SaveChanges();
                    UserKey = tmp.Id;
                    return View("Views/Home/SignUp/SignedUp.cshtml",r);
                }
            }
            else
                return View("~/Views/Home/SignUp/SignUp.cshtml");
        }

        [HttpGet]
        [Route("Authentication/LogIn")]
        public ViewResult LogIn()
        {
            return View("~/Views/Home/LogIn/LogIn.cshtml");
        }

        [HttpPost]
        [Route("Authentication/LogIn")]
        public ViewResult LogIn(ModelUser r)
        {
           var tmp = appDbContext.Users.Where(x => x.Login == r.LoginBuilder).FirstOrDefault();
           if (tmp != null && tmp.Password == r.PasswordBuilder) 
           {
                r.NameBuilder = tmp.Name;
                UserKey = tmp.Id;
                return View("~/Views/Home/Login/LoggedIn.cshtml", r);
           }
            else
                return View("~/Views/Home/Login/Error.cshtml");
        }

        [HttpGet]
        [Route("Authentication/Navigator")]
        public ViewResult Navigator()
        {
            var tmp = (from r in appDbContext.Roles
                       join u in appDbContext.Users
                       on r.Id equals u.RoleId
                       where u.Id == UserKey
                       select r).FirstOrDefault();
            if (tmp.Name == "Admin")
                return View("~/Views/Home/Navidation/AdminHomeScreen.cshtml");
            else if (tmp.Name == "TeamLead")
                return View("~/Views/Home/Navidation/TeamLeaderHomeScreen.cshtml", UserKey);
            else if (tmp.Name == "Customer")
                return View("~/Views/Home/Navidation/CustomerHomeScreen.cshtml",UserKey);
            else if (tmp.Name == "Middle" || tmp.Name == "Junior" || tmp.Name == "Senior")
                return View("~/Views/Home/Navidation/WorkerHomeScreen.cshtml", UserKey);
            else
                return View("~/Views/Home/Login/Error.cshtml");
        }

    }
    
}

