using Microsoft.AspNetCore.Mvc;
using CourseWork.Models;
using CourseWork.Interfaces;

namespace CourseWork.Controllers
{
    public class RepositoryController : Controller
    {
        private readonly IRepositoryService _repositoryService;

        public RepositoryController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        private static int _userKey;

        [HttpGet]
        [Route("Repository/SeeRepositoriesAsLeader/{id}")]
        public ViewResult SeeRepositoriesAsLeader(int id)
        {
            _userKey = id;
            var tmp = _repositoryService.SeeRepositoriesAsLeader(id);
            return View("~/Views/Home/Repository/SeeRepositoriesAsLeader.cshtml",tmp);
        }

        [HttpGet]
        [Route("Repository/SeeRepositoriesAsWorker/{id}")]
        public ViewResult SeeRepositoriesAsWorker(int id)
        {
            var tmp = _repositoryService.SeeRepositoriesAsWorker(id);
            if (tmp != null)
                return View("~/Views/Home/Repository/SeeRepositoriesAsWorker.cshtml", tmp);
            return View("~/Views/Home/Repository/NoRepository.cshtml");
        }

        [HttpPost]
        [Route("Repository/DeleteRepository")]
        public ViewResult DeleteRepository(int id)
        {
            var result = _repositoryService.DeleteRepository(id);
            if (result)
                return View("~/Views/Home/Repository/DeletedRepository.cshtml", _userKey);
            return View("~/Views/Home/Error.cshtml");
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
                var result = _repositoryService.CreateRepository(r, _userKey);
                if (result)
                    return View("~/Views/Home/Repository/CreatedRepository.cshtml", _userKey);
                else
                    return View("~/Views/Home/Repository/NoRepository.cshtml", _userKey);
            }
            return View("~/Views/Home/Repository/CreateRepository.cshtml");
        }

        [HttpGet]
        [Route("Repository/EditRepository/{id}")]
        public ViewResult EditRepository(int id)
        {
            var tmp = _repositoryService.GetRepository(id);
            if (tmp != null)
                return View("~/Views/Home/Repository/EditRepository.cshtml", tmp);
            return View("~/Views/Home/Error.cshtml");
        }

        [HttpPost]
        [Route("Repository/EdiedRepository")]
        public ViewResult EditedRepository(ModelRepository mr)
        {
            if (ModelState.IsValid)
            {
                var result = _repositoryService.EditRepository(mr);
                if (result)
                    return View("~/Views/Home/Repository/EditedRepository.cshtml", _userKey);
                else
                    return View("~/Views/Home/Repository/NoRepository.cshtml");
            }
            return View("~/Views/Home/Repository/EditRepository.cshtml", mr);
        }

    }
}

