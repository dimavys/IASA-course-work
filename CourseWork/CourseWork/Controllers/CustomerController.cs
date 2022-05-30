using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Models;
using CourseWork.Data;

namespace CourseWork.Controllers
{
    public class CustomerController : Controller
    {
        private AppDbContext appDbContext;

        private static int userKey = AuthenticationController.UserKey;

        public CustomerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("Customer/SeeCustomers")]
        public ViewResult SeeCustomers()
        {
            var tmp = (from u in appDbContext.Users
                       join r in appDbContext.Roles
                       on u.RoleId equals r.Id
                       where r.Name == "Customer"
                       select new ModelUser
                       {
                           IdBuilder = u.Id,
                           NameBuilder = u.Name,
                           SurnameBuilder = u.Surname,
                           LoginBuilder = u.Login
                       }).ToList();
            return View("~/Views/Home/Customer/SeeCustomer.cshtml",tmp);
        }
    }
}

