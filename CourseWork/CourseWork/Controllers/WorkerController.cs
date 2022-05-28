using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Data;
using CourseWork.Models;

namespace CourseWork.Controllers
{
    public class WorkerController : Controller
    {
        private AppDbContext appDbContext;

        public WorkerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("Worker/CreateWorker")]
        public ViewResult CreateWorker()
        {
            return View("~/Views/Home/Worker/CreateWorker.cshtml");
        }

        [HttpPost]
        [Route("Worker/CreateWorker")]
        public ViewResult CreateWorker(ModelWorker w)
        {
            if (w.LoginBuilder != null)
            {
                var tmp = appDbContext.Users.Where(x => x.Login == w.LoginBuilder).FirstOrDefault();
                if (tmp != null)
                    return View("~/Views/Home/Worker/NoWorker.cshtml");
                else
                {
                    var tp = new User();
                    var r = appDbContext.Roles.Where(x => x.Name == w.RoleBuilder).FirstOrDefault();
                    w.CopyData(tp, r.Id);
                    appDbContext.Users.Add(tp);
                    appDbContext.SaveChanges();
                    return View("~/Views/Home/Worker/CreatedWorker.cshtml", w);
                }
            }
            else
                return View("~/Views/Home/Worker/CreateWorker.cshtml");
        }

        [HttpGet]
        [Route("Worker/SeeWorkers")]
        public ViewResult SeeWorkers()
        {
            var tmp = (from u in appDbContext.Users
                       join r in appDbContext.Roles
                       on u.RoleId equals r.Id
                       where r.Name != "Customer"
                       select new ModelWorker
                       {
                           IdBuilder = u.Id,
                           NameBuilder = u.Name,
                           SurnameBuilder = u.Surname,
                           LoginBuilder = u.Login,
                           RoleBuilder = r.NormalizedName,
                           SalaryBuilder = (double)u.Salary
                       }).ToList();
            return View("~/Views/Home/Worker/SeeWorkers.cshtml", tmp);
        }

        [HttpPost]
        [Route("Worker/Deletion")]
        public ViewResult Deletion(int id)
        {
            var tmp = appDbContext.Users.Where(x => x.Id == id).FirstOrDefault();
            appDbContext.Users.Remove(tmp);
            appDbContext.SaveChanges();
            return View("~/Views/Home/Worker/DeletedWorker.cshtml");
        }

        [HttpGet]
        [Route("Worker/Edition/{id}")]
        public ViewResult Edition(int id)
        {
            var tmp = (from u in appDbContext.Users
                       join r in appDbContext.Roles
                       on u.RoleId equals r.Id
                       where u.Id == id
                       select new ModelWorker
                       {
                           IdBuilder = u.Id,
                           LoginBuilder = u.Login,
                           PasswordBuilder = u.Password,
                           NameBuilder = u.Name,
                           SurnameBuilder = u.Surname,
                           RoleBuilder = r.Name,
                           SalaryBuilder = (double)u.Salary
                       }).FirstOrDefault();
            return View("~/Views/Home/Worker/EditionWorker.cshtml", tmp);
        }

        [HttpPost]
        [Route("Worker/Edited")]
        public ViewResult Edited(ModelWorker w)
        {
            var tmp = appDbContext.Users.Where(x => x.Id == w.IdBuilder).FirstOrDefault();
            var rl = appDbContext.Roles.Where(x => x.Name == w.RoleBuilder).FirstOrDefault();
            w.CopyData(tmp, rl.Id);
            appDbContext.SaveChanges();
            return View("~/Views/Home/Worker/EditedWorker.cshtml",w);
        }
    }
}

