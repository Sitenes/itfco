using itfco.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace itfco.Controllers
{
    public class AdminController : Controller
    {

        public AdminController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
