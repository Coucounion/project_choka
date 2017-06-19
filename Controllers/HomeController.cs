using Microsoft.AspNetCore.Mvc;

namespace WebApplicationBasic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            int totalOutstandingUnits = 1823749;
            ViewData["Message"] = string.Format("There are currently {0} units in transit.", totalOutstandingUnits);
            ViewData["Title"] = "MetroPCS UnitTracker Project Choka";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
