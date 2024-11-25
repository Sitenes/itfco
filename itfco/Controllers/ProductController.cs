using itfco.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace itfco.Controllers
{
    public class ProductController : Controller
    {

        public ProductController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
