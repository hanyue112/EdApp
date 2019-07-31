using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EdAppWeb.Models;
using EdAppWeb.Interfaces;

namespace EdAppWeb.Controllers
{
    public class HomeController : Controller
    {
        private IAuctionOperations _acutionOperations;

        public HomeController(IAuctionOperations acutionOperations)
        {
            _acutionOperations = acutionOperations;
        }

        public IActionResult Index()
        {
            return View(_acutionOperations.Render());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
