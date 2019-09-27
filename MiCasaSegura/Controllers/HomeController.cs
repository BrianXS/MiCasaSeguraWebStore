using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiCasaSegura.Services.Database;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiCasaSegura.Controllers
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
