using CourseWork.Interfaces;
using CourseWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        private static int userKey;

        [HttpGet]
        [Route("Project/SeeProjects")]
        public ViewResult SeeProjects()
        {
            var tmp = _projectService.SeeProjects();
            if (tmp != null)
                return View("~/Views/Home/Project/SeeProjects.cshtml", tmp);
            else
                return View("~/Views/Home/Project/CreateProject.cshtml");
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
                var result = _projectService.CreateProject(p);
                if (result)
                    return View("~/Views/Home/Project/CreatedProject.cshtml",p);
                else
                    return View("~/Views/Home/Project/NoProject.cshtml");
            }
            else
                return View("~/Views/Home/Project/CreateProject.cshtml");
        }

        [HttpPost]
        [Route("Project/DeleteProject")]
        public ViewResult DeleteProject(int id)
        {
            var result = _projectService.DeleteProject(id);
            if (result) 
                return View("~/Views/Home/Project/DeletedProject.cshtml");
            else
                return View("~/Views/Home/Error.cshtml");
        }

        [HttpGet]
        [Route("Project/SeeProjectAsLeader/{id}")]
        public ViewResult SeeProjectAsLeader(int id)
        {
            userKey = id;
            var tmp = _projectService.SeeProjectAsLeader(id);
            if (tmp != null)
                return View("~/Views/Home/Project/SeeProject.cshtml", tmp);
            else
                return View("~/Views/Home/Project/NotYetProject.cshtml");
        }

        [HttpGet]
        [Route("Project/SeeProjectAsCustomer/{id}")]
        public ViewResult SeeProjectAsCustomer(int id)
        {
            var tmp = _projectService.SeeProjectAsCustomer(id);
            if (tmp != null)
                return View("~/Views/Home/Project/SeeProjectCustomer.cshtml", tmp);
            else
                return View("~/Views/Home/Project/NotYetProject.cshtml");
        }

        [HttpGet]
        [Route("Project/SeeProjectsAsWorker/{id}")]
        public ViewResult SeeProjectsAsWorker(int id)
        {
            var tmp = _projectService.SeeProjectsAsWorker(id);
            if (tmp != null)
                return View("~/Views/Home/Project/SeeProjectsAsWorker.cshtml",tmp);
            else
                return View("~/Views/Home/Project/NotYetProject.cshtml");
        }

        [HttpGet]
        [Route("Project/EditProject/{id}")]
        public ViewResult EditProject(int id)
        {
            var tmp = _projectService.GetProject(id);
            if (tmp != null)
                return View("~/Views/Home/Project/EditProject.cshtml", tmp);
            else
                return View("~/Views/Home/Error.cshtml");
        }

        [HttpPost]
        [Route("Project/EditedProject")]
        public ViewResult EditedProject(ModelProject p)
        {
            if (ModelState.IsValid)
            {
                var result = _projectService.EditProject(p);
                if (result)
                    return View("~/Views/Home/Project/EditedProject.cshtml", userKey);
                else
                    return View("~/Views/Home/Project/NoProject.cshtml", p);
            }
            else
                return View("~/Views/Home/Project/EditProject.cshtml", p);
        }
    }
}

