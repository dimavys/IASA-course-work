using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Models;
using CourseWork.Data;

namespace CourseWork.Controllers;

public class HomeController : Controller
{
    private AppDbContext appDbContext;

    public ViewResult Index()
    {
        return View();
    }
}

