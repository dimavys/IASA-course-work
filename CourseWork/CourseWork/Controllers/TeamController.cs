using Microsoft.AspNetCore.Mvc;
using CourseWork.Models;
using CourseWork.Interfaces;

namespace CourseWork.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
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
                var result = _teamService.CreateTeam(t);
                if (result)
                    return View("~/Views/Home/Team/CreatedTeam.cshtml", t);
                else
                    return View("~/Views/Home/Team/NoTeam.cshtml");
            }
            else
                return View("~/Views/Home/Team/CreateTeam.cshtml");
        }

        [HttpGet]
        [Route("Team/SeeTeams")]
        public ViewResult SeeTeams()
        {
            var tmp = _teamService.SeeTeams();
            return View("~/Views/Home/Team/SeeTeams.cshtml", tmp);
        }

        [HttpGet]
        [Route("Team/SeeTeamsAsWorker/{id}")]
        public ViewResult SeeTeamsAsWorker(int id)
        {
            var tmp = _teamService.SeeTeamsAsWorker(id);
            if (tmp != null)
                return View("~/Views/Home/Team/SeeTeamsAsWorker.cshtml", tmp);
            else
                return View("~/Views/Home/Team/NotYetTeam.cshtml");
        }


        [HttpPost]
        [Route("Team/DeleteTeam")]
        public ViewResult DeleteTeam(int id)
        {
            var result = _teamService.DeleteTeam(id);
            if (result)
                return View("~/Views/Home/Team/DeletedTeam.cshtml");
            else
                return View("~/Views/Home/Error.cshtml");
        }

        [HttpGet]
        [Route("Team/SeeWorkers/{id}")]
        public ViewResult SeeWorkers(int id)
        {
            TeamKey = id;
            var tmp = _teamService.SeeWorkers(id);
            if (tmp != null)
                 return View("~/Views/Home/Team/SeeWorkers.cshtml",tmp);
            else
                return View("~/Views/Home/Team/NoWorker.cshtml");
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
            if (w.LoginBuilder != null)
            {
                var result = _teamService.AddWorker(TeamKey, w);
                if (result)
                {
                    return View("~/Views/Home/Team/AddedWorker.cshtml");
                }
                else
                    return View("~/Views/Home/Team/NoWorker.cshtml");
            }
            else
                return View("~/Views/Home/Team/AddWorker.cshtml");
        }

        [HttpPost]
        [Route("Team/RemoveWorker")]
        public ViewResult RemoveWorker(int id)
        {
            var result = _teamService.RemoveWorker(TeamKey, id);
            if (result)
                return View("~/Views/Home/Team/RemovedWorker.cshtml");
            else
                return View("~/Views/Home/Error.cshtml");
        }
    }
}

