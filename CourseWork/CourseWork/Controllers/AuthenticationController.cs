using Microsoft.AspNetCore.Mvc;
using CourseWork.Models;
using CourseWork.Interfaces;

namespace CourseWork.Controllers
{
    public class AuthenticationController : Controller
    {
        private static int _userKey;

        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("Authentication/SignUp")]
        public ViewResult SignUp()
        {
            return View("~/Views/Home/SignUp/SignUp.cshtml");
        }

        [HttpPost]
        [Route("Authentication/SignUp")]
        public ViewResult SignUp(ModelUser r)
        {
            if (ModelState.IsValid && r.RoleBuilder == r.PasswordBuilder)
            {
                var result = _authService.SignUp(r);
                if (result)
                {
                    _userKey = _authService.GetId(r);
                    return View("~/Views/Home/SignUp/SignedUp.cshtml", _authService.GetUser(_userKey));
                }
                else
                    return View("~/Views/Home/SignUp/Error.cshtml");
            }
            else
                return View("~/Views/Home/SignUp/SignUp.cshtml");
        }

        [HttpGet]
        [Route("Authentication/LogIn")]
        public ViewResult LogIn()
        {
            return View("~/Views/Home/LogIn/LogIn.cshtml");
        }

        [HttpPost]
        [Route("Authentication/LogIn")]
        public ViewResult LogIn(ModelUser r)
        {
           var result = _authService.LogIn(r);
           if (result)
           {
                _userKey = _authService.GetId(r);
                return View("~/Views/Home/Login/LoggedIn.cshtml", _authService.GetUser(_userKey));
           }
            else
                return View("~/Views/Home/Login/Error.cshtml");
        }

        [HttpGet]
        [Route("Authentication/Navigator")]
        public ViewResult Navigator()
        {
            var tmp = _authService.GetRoleName(_userKey);
            if (tmp == "Admin")
                return View("~/Views/Home/Navidation/AdminHomeScreen.cshtml",_userKey);
            else if (tmp == "TeamLead")
                return View("~/Views/Home/Navidation/TeamLeaderHomeScreen.cshtml", _userKey);
            else if (tmp == "Customer")
                return View("~/Views/Home/Navidation/CustomerHomeScreen.cshtml",_userKey);
            else if (tmp == "Middle" || tmp == "Junior" || tmp == "Senior")
                return View("~/Views/Home/Navidation/WorkerHomeScreen.cshtml", _userKey);
            else
                return View("~/Views/Home/Login/Error.cshtml");
        }

    }
    
}

