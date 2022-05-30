using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Data;
using CourseWork.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseWork.Controllers
{
    public class TaskController : Controller
    {
        private AppDbContext appDbContext;

        private static int repositoryKey;

        public TaskController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("Task/SeeTasksAsLeader/{id}")]
        public ViewResult SeeTasksAsLeader(int id)
        {
            repositoryKey = id;
            var tmp = (from t in appDbContext.Tasks
                       where t.RepositoryId == id
                       orderby t.Priority
                       select new ModelTask
                       {
                           IdBuilder = t.Id,
                           TitleBuilder = t.Title,
                           DescBuilder = t.Description,
                           StartBuilder = t.StartDate,
                           FinishBuilder = t.FinishDate,
                           PriorityBuilder = t.Priority
                       }).ToList();
            return View("~/Views/Home/Task/SeeTasksAsLeader.cshtml", tmp);
        }

        [HttpGet]
        [Route("Task/SeeTasksAsWorker/{id}")]
        public ViewResult SeeTasksAsWorker(int id)
        {
            repositoryKey = id;
            var tmp = (from t in appDbContext.Tasks
                       where t.RepositoryId == id
                       orderby t.Priority
                       select new ModelTask
                       {
                           TitleBuilder = t.Title,
                           DescBuilder = t.Description,
                           StartBuilder = t.StartDate,
                           FinishBuilder = t.FinishDate,
                           PriorityBuilder = t.Priority
                       }).ToList();
            return View("~/Views/Home/Task/SeeTasksAsWorker.cshtml", tmp);
        }

        [HttpGet]
        [Route("Task/CreateTask")]
        public ViewResult CreateTask()
        {
            return View("~/Views/Home/Task/CreateTask.cshtml");
        }

        [HttpPost]
        [Route("Task/CreateTask")]
        public ViewResult CreateTask(ModelTask mt)
        {
            if (ModelState.IsValid)
            {
                var tmp = appDbContext.Tasks.Where(x => x.Title == mt.TitleBuilder && x.RepositoryId == repositoryKey).FirstOrDefault();
                if (tmp != null)
                    return View("~/Views/Home/Task/CreateTask.cshtml");
                else
                {
                    var task = new Data.Task();
                    mt.CopyTask(task, repositoryKey);
                    appDbContext.Tasks.Add(task);
                    appDbContext.SaveChanges();
                    return View("~/Views/Home/Task/CreatedTask.cshtml", repositoryKey);
                }
            }
            else
                return View("~/Views/Home/Task/CreateTask.cshtml");
        }

        [HttpGet]
        [Route("Task/EditTask/{id}")]
        public ViewResult EditTask(int id)
        {
            var tmp = (from t in appDbContext.Tasks
                       where t.Id == id
                       select new ModelTask
                       {
                           IdBuilder = t.Id,
                           TitleBuilder = t.Title,
                           DescBuilder = t.Description,
                           StartBuilder = t.StartDate,
                           FinishBuilder = t.FinishDate,
                           PriorityBuilder = t.Priority
                       }).FirstOrDefault();
            return View("~/Views/Home/Task/EditTask.cshtml", tmp);
        }

        [HttpPost]
        [Route("Task/EditedTask")]
        public ViewResult EditedTask(ModelTask mt)
        {
            if (ModelState.IsValid)
            {
                var tmp = appDbContext.Tasks.Where(x => x.Id == mt.IdBuilder).FirstOrDefault();
                mt.CopyTask(tmp, tmp.RepositoryId);
                appDbContext.SaveChanges();
                return View("~/Views/Home/Task/EditedTask.cshtml", repositoryKey);
            }
            else
                return View("~/Views/Home/Task/EditTask.cshtml", mt);
        }

        [HttpPost]
        [Route("Task/DeleteTask")]
        public ViewResult DeleteTask(int id)
        {
            var tmp = appDbContext.Tasks.Where(x => x.Id == id).FirstOrDefault();
            appDbContext.Tasks.Remove(tmp);
            appDbContext.SaveChanges();
            return View("~/Views/Home/Task/DeletedTask.cshtml", repositoryKey);
        }
    }
}

