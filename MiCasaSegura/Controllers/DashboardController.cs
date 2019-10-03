using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiCasaSegura.Filters;
using MiCasaSegura.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiCasaSegura.Controllers
{
    [Authorize, ServiceFilter(typeof(MissingUserInfo))]
    //[Authorize]
    public class DashboardController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public DashboardController(UserManager<User> userManager,
                                   SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
