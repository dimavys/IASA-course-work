using Microsoft.AspNetCore.Mvc;
using CourseWork.Models;
using CourseWork.Interfaces;

namespace CourseWork.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IWorkerService _workerService;

        private static int _userKey;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
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
                var result = _workerService.CreateWorker(w);
                if (result)
                    return View("~/Views/Home/Worker/CreatedWorker.cshtml", _userKey);
                else
                    return View("~/Views/Home/Worker/NoWorker.cshtml");
            }
            return View("~/Views/Home/Worker/CreateWorker.cshtml");
        }

        [HttpGet]
        [Route("Worker/SeeWorkers/{id}")]
        public ViewResult SeeWorkers(int id)
        {
            _userKey = id;
            var tmp = _workerService.SeeWorkers(id);
            return View("~/Views/Home/Worker/SeeWorkers.cshtml", tmp);
        }

        [HttpGet]
        [Route("Worker/SeeWorkersInProject/{id}")]
        public ViewResult SeeWorkersInProject(int id)
        {
            var tmp = _workerService.SeeWorkersInProject(id);
            if (tmp != null)
                return View("~/Views/Home/Worker/SeeLeaderWorkers.cshtml", tmp);
            return View("~/Views/Home/Worker/NotYetWorker.cshtml");
        }

        [HttpPost]
        [Route("Worker/Deletion")]
        public ViewResult Deletion(int id)
        {
            var result = _workerService.DeleteWorker(id);
            if (result)
                return View("~/Views/Home/Worker/DeletedWorker.cshtml",_userKey);
            return View("~/Views/Home/Error.cshtml");
        }

        [HttpGet]
        [Route("Worker/Edition/{id}")]
        public ViewResult Edition(int id)
        {
            var tmp = _workerService.EditWorker(id);
            if (tmp != null)
                return View("~/Views/Home/Worker/EditionWorker.cshtml", tmp);
            return View("~/Views/Home/Worker/NoWorker.cshtml");
        }

        [HttpPost]
        [Route("Worker/Edited")]
        public ViewResult Edited(ModelWorker w)
        {
            var result = _workerService.EditWorker(w);
            if (result)
                return View("~/Views/Home/Worker/EditedWorker.cshtml", _userKey);
            return View("~/Views/Home/Worker/NoWorker.cshtml");
        }
    }
}

