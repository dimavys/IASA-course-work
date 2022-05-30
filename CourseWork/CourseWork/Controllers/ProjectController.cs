using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Data;
using CourseWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    public class ProjectController : Controller
    {
        private AppDbContext appDbContext;

        public ProjectController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("Project/SeeProjects")]
        public ViewResult SeeProjects()
        {
            var tmp = (from p in appDbContext.Projects
                       join t in appDbContext.Teams
                       on p.TeamId equals t.Id
                       select new ModelProject
                       {
                           IdBuilder = p.Id,
                           NameBuilder = p.Name,
                           TeamBuilder = t.Name,
                           RegBuilder = p.Regulations,
                           DescBuilder = p.Description,
                           StatusBuilder = p.Status
                       }).ToList();
            return View("~/Views/Home/Project/SeeProjects.cshtml", tmp);
        }

        [HttpGet]
        [Route("Project/CreateProject")]
        public ViewResult CreateProject()
        {
            return View("~/Views/Home/Project/CreateProject.cshtml");
        }

        [HttpPost]
        [Route("Project/CreateProject")]
        public ViewResult CreateProject(ModelProject p)
        {
            if (ModelState.IsValid)
            {
                var t = appDbContext.Teams.Where(x => x.Name == p.TeamBuilder).FirstOrDefault();
                if (t != null)
                {
                    var tp = appDbContext.Projects.Where(x => x.Name == p.NameBuilder).FirstOrDefault();
                    if (tp != null)
                        return View("~/Views/Home/Project/NoProject.cshtml");
                    else
                    {
                        var tmp = new Project();
                        p.CopyProject(tmp, t.Id);
                        appDbContext.Projects.Add(tmp);
                        appDbContext.SaveChanges();
                        return View("~/Views/Home/Project/CreatedProject.cshtml",p);
                    }
                }
                else
                    return View("~/Views/Home/Project/NoTeam.cshtml");
            }
            else
                return View("~/Views/Home/Project/CreateProject.cshtml");
        }

        [HttpPost]
        [Route("Project/DeleteProject")]
        public ViewResult DeleteProject(int id)
        {
            var tmp = appDbContext.Projects.Where(x => x.Id == id).FirstOrDefault();
            appDbContext.Projects.Remove(tmp);
            appDbContext.SaveChanges();
            return View("~/Views/Home/Project/DeletedProject.cshtml");
        }

        [HttpGet]
        [Route("Project/SeeProjectAsLeader/{id}")]
        public ViewResult SeeProjectAsLeader(int id)
        {
            var tmp = (from p in appDbContext.Projects
                       join t in appDbContext.Teams
                       on p.TeamId equals t.Id
                       where t.TeamLeadId == id
                       select new ModelProject
                       {
                           IdBuilder = p.Id,
                           NameBuilder = p.Name,
                           TeamBuilder = t.Name,
                           RegBuilder = p.Regulations,
                           DescBuilder = p.Description,
                           StatusBuilder = p.Status
                       }).FirstOrDefault();
            if (tmp != null)
                return View("~/Views/Home/Project/SeeProject.cshtml", tmp);
            else
                return View("~/Views/Home/Project/NotYetProject.cshtml");
        }

        [HttpGet]
        [Route("Project/SeeProjectAsCustomer/{id}")]
        public ViewResult SeeProjectAsCustomer(int id)
        {
            var tmp = (from p in appDbContext.Projects
                       join t in appDbContext.Teams
                       on p.TeamId equals t.Id
                       where t.CustomerId == id
                       select new ModelProject
                       {
                           IdBuilder = p.Id,
                           NameBuilder = p.Name,
                           TeamBuilder = t.Name,
                           RegBuilder = p.Regulations,
                           DescBuilder = p.Description,
                           StatusBuilder = p.Status
                       }).FirstOrDefault();
            if (tmp != null)
                return View("~/Views/Home/Project/SeeProjectCustomer.cshtml", tmp);
            else
                return View("~/Views/Home/Project/NotYetProject.cshtml");
        }

        [HttpGet]
        [Route("Project/SeeProjectsAsWorker/{id}")]
        public ViewResult SeeProjectsAsWorker(int id)
        {
            var tmp = (from u in appDbContext.Users
                       join wks in appDbContext.Workings
                       on u.Id equals wks.WorkerId
                       join t in appDbContext.Teams
                       on wks.TeamId equals t.Id
                       join p in appDbContext.Projects
                       on t.Id equals p.TeamId
                       where u.Id == id
                       select new ModelProject
                       {
                           IdBuilder = p.Id,
                           NameBuilder = p.Name,
                           TeamBuilder = t.Name,
                           RegBuilder = p.Regulations,
                           DescBuilder = p.Description,
                           StatusBuilder = p.Status
                       }).ToList();
                return View("~/Views/Home/Project/SeeProjectsAsWorker.cshtml",tmp);
        }

        [HttpGet]
        [Route("Project/EditProject/{id}")]
        public ViewResult EditProject(int id)
        {
            var tmp = (from p in appDbContext.Projects
                       join t in appDbContext.Teams
                       on p.TeamId equals t.Id
                       where p.Id == id
                       select new ModelProject
                       {
                           IdBuilder = p.Id,
                           NameBuilder = p.Name,
                           RegBuilder = p.Regulations,
                           DescBuilder = p.Description,
                           StatusBuilder = p.Status,
                           TeamBuilder = t.Name
                       }).FirstOrDefault();
            return View("~/Views/Home/Project/EditProject.cshtml", tmp);
        }

        [HttpPost]
        [Route("Project/EditedProject")]
        public ViewResult EditedProject(ModelProject p)
        {
            var tmp = appDbContext.Projects.Where(x => x.Id == p.IdBuilder).FirstOrDefault();
            if (ModelState.IsValid)
            {
                p.CopyProject(tmp);
                appDbContext.SaveChanges();
                return View("~/Views/Home/Project/EditedProject.cshtml", p);
            }
            else
                return View("~/Views/Home/Project/EditProject.cshtml", p);
        }
    }
}

