using Microsoft.AspNetCore.Mvc;
using CourseWork.Interfaces;

namespace CourseWork.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("Customer/SeeCustomers")]
        public ViewResult SeeCustomers()
        {
            var tmp = _customerService.SeeCustomers();
            if (tmp != null)
                return View("~/Views/Home/Customer/SeeCustomer.cshtml", tmp);
            else
                return View("~/Views/Home/Customer/NoCustomer.cshtml", tmp);
        }
    }
}

