using Microsoft.AspNetCore.Mvc;
using CourseWork.Models;
using CourseWork.Interfaces;

namespace CourseWork.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        private static int _repositoryKey;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("Task/SeeTasksAsLeader/{id}")]
        public ViewResult SeeTasksAsLeader(int id)
        {
            _repositoryKey = id;
            var tmp = _taskService.GetTasks(id);
            return View("~/Views/Home/Task/SeeTasksAsLeader.cshtml", tmp);
        }

        [HttpGet]
        [Route("Task/SeeTasksAsWorker/{id}")]
        public ViewResult SeeTasksAsWorker(int id)
        {
            _repositoryKey = id;
            var tmp = _taskService.GetTasks(id);
            if (tmp != null)
                return View("~/Views/Home/Task/SeeTasksAsWorker.cshtml", tmp);
            return View("~/Views/Home/Task/NotYetTask.cshtml", tmp);
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
                var result = _taskService.CreateTask(mt, _repositoryKey);
                if (result)
                    return View("~/Views/Home/Task/CreatedTask.cshtml", _repositoryKey);
                else
                    return View("~/Views/Home/Task/NoTask.cshtml");
            }
            return View("~/Views/Home/Task/CreateTask.cshtml");
        }

        [HttpGet]
        [Route("Task/EditTask/{id}")]   
        public ViewResult EditTask(int id)
        {
            var tmp = _taskService.GetTask(id);
            if (tmp != null)
                return View("~/Views/Home/Task/EditTask.cshtml", tmp);
            return View("~/Views/Home/Error.cshtml");
        }

        [HttpPost]
        [Route("Task/EditedTask")]
        public ViewResult EditedTask(ModelTask mt)
        {
            if (ModelState.IsValid)
            {
                var result = _taskService.EditTask(mt);
                if (result)
                    return View("~/Views/Home/Task/EditedTask.cshtml", _repositoryKey);
                else
                    return View("~/Views/Home/Error");
            }
            return View("~/Views/Home/Task/EditTask.cshtml", mt);
        }

        [HttpPost]
        [Route("Task/DeleteTask")]
        public ViewResult DeleteTask(int id)
        {
            var result = _taskService.DeleteTask(id);
            if (result)
                return View("~/Views/Home/Task/DeletedTask.cshtml", _repositoryKey);
            return View("~/Views/Home/Error.cshtml");
        }
    }
}

