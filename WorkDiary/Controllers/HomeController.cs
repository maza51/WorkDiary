using System;
using Microsoft.AspNetCore.Mvc;

namespace WorkDiary.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Months");
        }
    }
}

