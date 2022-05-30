using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Data;
using CourseWork.Models;

namespace CourseWork.Controllers
{
    public class RepositoryController : Controller
    {
        private AppDbContext appDbContext;

        public RepositoryController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        private static int userKey;

        [HttpGet]
        [Route("Repository/SeeRepositoriesAsLeader/{id}")]
        public ViewResult SeeRepositoriesAsLeader(int id)
        {
            userKey = id;
            var tmp = (from t in appDbContext.Teams
                       join p in appDbContext.Projects
                       on t.Id equals p.TeamId
                       join r in appDbContext.Repositories
                       on p.Id equals r.ProjectId
                       where t.TeamLeadId == id
                       select new ModelRepository
                       {
                           IdBuilder = r.Id,
                           NameBuilder = r.Name
                       }).ToList();
            return View("~/Views/Home/Repository/SeeRepositoriesAsLeader.cshtml",tmp);
        }

        [HttpGet]
        [Route("Repository/SeeRepositoriesAsWorker/{id}")]
        public ViewResult SeeRepositoriesAsWorker(int id)
        {
            var tmp = (from r in appDbContext.Repositories
                       where r.Id == id
                       select new ModelRepository
                       {
                           IdBuilder = r.Id,
                           NameBuilder = r.Name
                       }).ToList();
            return View("~/Views/Home/Repository/SeeRepositoriesAsWorker.cshtml", tmp);
        }

        [HttpPost]
        [Route("Repository/DeleteRepositoty")]
        public ViewResult DeleteRepository(int id)
        {
            var tmp = appDbContext.Repositories.Where(x => x.Id == id).FirstOrDefault();
            appDbContext.Repositories.Remove(tmp);
            appDbContext.SaveChanges();
            return View("~/Views/Home/Repository/DeletedRepository.cshtml",userKey);
        }

        [HttpGet]
        [Route("Repository/CreateRepository")]
        public ViewResult CreateRepository()
        { 
            return View("~/Views/Home/Repository/CreateRepository.cshtml");
        }

        [HttpPost]
        [Route("Repository/CreateRepository")]
        public ViewResult CreateRepository(ModelRepository r)
        {
            if (ModelState.IsValid)
            {
                var tp = appDbContext.Repositories.Where(x => x.Name == r.NameBuilder).FirstOrDefault();
                if (tp != null)
                    return View("~/Views/Home/Repository/NoRepository.cshtml");
                else
                {
                    var rep = new Repository();
                    var pId = (from t in appDbContext.Teams
                               join p in appDbContext.Projects
                               on t.Id equals p.TeamId
                               where t.TeamLeadId == userKey
                               select p.Id).FirstOrDefault();
                    r.CopyRepository(rep,pId);
                    appDbContext.Repositories.Add(rep);
                    appDbContext.SaveChanges();
                    return View("~/Views/Home/Repository/CreatedRepository.cshtml",userKey);
                }
            }
            else 
                return View("~/Views/Home/Repository/CreateRepository.cshtml");
        }

        [HttpGet]
        [Route("Repository/EditRepository/{id}")]
        public ViewResult EditRepository(int id)
        {
            var tmp = (from r in appDbContext.Repositories
                       select new ModelRepository
                       {
                           NameBuilder = r.Name,
                           IdBuilder = r.Id
                       }).FirstOrDefault();
            return View("~/Views/Home/Repository/EditRepository.cshtml",tmp);
        }

        [HttpPost]
        [Route("Repository/EdiedRepository")]
        public ViewResult EditedRepository(ModelRepository mr)
        {
            if (ModelState.IsValid)
            {
                var tmp = appDbContext.Repositories.Where(x => x.Id == mr.IdBuilder).FirstOrDefault();
                mr.CopyRepository(tmp, tmp.ProjectId);
                appDbContext.SaveChanges();
                return View("~/Views/Home/Repository/EditedRepository.cshtml", userKey);
            }
            else
                return View("~/Views/Home/Repository/EditRepository.cshtml", mr);
        }

    }
}

