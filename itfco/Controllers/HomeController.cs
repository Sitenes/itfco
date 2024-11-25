using itfco.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace itfco.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
