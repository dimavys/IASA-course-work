using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Data;
using CourseWork.Models;

namespace CourseWork.Controllers
{
    public class TeamController : Controller
    {
        private AppDbContext appDbContext;

        public TeamController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        private static int TeamKey;

        [HttpGet]
        [Route("Team/CreateTeam")]
        public ViewResult CreateTeam()
        {
            return View("~/Views/Home/Team/CreateTeam.cshtml");
        }

        [HttpPost]
        [Route("Team/CreateTeam")]
        public ViewResult CreateTeam(ModelTeam t)
        {
            if (ModelState.IsValid)
            {
                var client = appDbContext.Users.Where(x => x.Login == t.CustomerLoginBuilder).FirstOrDefault();
                var leader = appDbContext.Users.Where(x => x.Login == t.TeamLeadLoginBuilder).FirstOrDefault();
                if (client != null && leader != null)
                {
                    var tm = appDbContext.Teams.Where(x => x.Name == t.NameBuilder || x.CustomerId == client.Id || x.TeamLeadId == leader.Id).FirstOrDefault();
                    if (tm != null)
                        return View("~/Views/Home/Team/NoTeam.cshtml");
                    else
                    {
                        var tmp = new Team();
                        t.CopyTeam(tmp, client.Id, leader.Id);
                        appDbContext.Teams.Add(tmp);
                        appDbContext.SaveChanges();
                        return View("~/Views/Home/Team/AddedTeam.cshtml", t);
                    }
                        
                }
                   return View("~/Views/Home/Team/NoTeam.cshtml"); 
            }
            else
                return View("~/Views/Home/Team/CreateTeam.cshtml");
        }

        [HttpGet]
        [Route("Team/SeeTeams")]
        public ViewResult SeeTeams()
        {
            var tmp = (from u in appDbContext.Users
                       join t in appDbContext.Teams
                       on u.Id equals t.CustomerId
                       join u1 in appDbContext.Users
                       on t.TeamLeadId equals u1.Id
                       select new ModelTeam { IdBuilder = t.Id, NameBuilder = t.Name, RatingBuilder = t.Rating,
                           CustomerLoginBuilder = u.Login, TeamLeadLoginBuilder = u1.Login }).ToList() ;
            return View("~/Views/Home/Team/SeeTeams.cshtml", tmp);
        }

        [HttpPost]
        [Route("Team/Deletion")]
        public ViewResult Deletion(int id)
        {
            var tmp = appDbContext.Teams.Where(x => x.Id == id).FirstOrDefault();
            appDbContext.Teams.Remove(tmp);
            appDbContext.SaveChanges();
            return View("~/Views/Home/Team/DeletedTeam.cshtml");
        }

        [HttpGet]
        [Route("Team/SeeWorkers/{id}")]
        public ViewResult SeeWorkers(int id)
        {
            TeamKey = id;
            var tmp = (from r in appDbContext.Roles
                       join u in appDbContext.Users
                       on r.Id equals u.RoleId
                       join w in appDbContext.Workings
                       on u.Id equals w.WorkerId
                       where w.TeamId == id
                       select new ModelWorker
                       {
                           IdBuilder = u.Id,
                           LoginBuilder = u.Login,
                           PasswordBuilder = u.Password,
                           NameBuilder = u.Name,
                           SurnameBuilder = u.Surname,
                           RoleBuilder = r.Name,
                           SalaryBuilder = (double)u.Salary
                       }).ToList();
            return View("~/Views/Home/Team/SeeWorkers.cshtml",tmp);
        }

        [HttpGet]
        [Route("Team/AddWorker")]
        public ViewResult AddWorker()
        {
            return View("~/Views/Home/Team/AddWorker.cshtml");
        }

        [HttpPost]
        [Route("Team/AddWorker")]
        public ViewResult AddWorker(ModelWorker w)
        {
            if (ModelState.IsValid)
            {
                var _w = appDbContext.Users.Where(x => x.Login == w.LoginBuilder).FirstOrDefault();
                if (_w != null)
                {
                    var tmp = new Working();
                    tmp.TeamId = TeamKey;
                    tmp.WorkerId = _w.Id;
                    appDbContext.Workings.Add(tmp);
                    appDbContext.SaveChanges();
                    var t = new ModelTeam();
                    t.IdBuilder = tmp.TeamId;
                    return View("~/Views/Home/Team/AddedWorker.cshtml",t);
                }
                else
                    return View("~/Views/Home/Team/NoWorker.cshtml");
            }
            else
               return View("~/Views/Home/Team/AddWorker.cshtml");
        }
    }
}

