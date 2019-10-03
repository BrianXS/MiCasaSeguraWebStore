using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MiCasaSegura.Filters;
using MiCasaSegura.Models.Identity;
using MiCasaSegura.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiCasaSegura.Controllers
{
    public class AuthController : Controller
    {
        private IAuthenticationSchemeProvider authenticationSchemeProvider;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private RoleManager<Role> roleManager;

        public AuthController(IAuthenticationSchemeProvider authenticationSchemeProvider,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              RoleManager<Role> roleManager)
        {
            this.authenticationSchemeProvider = authenticationSchemeProvider;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [ServiceFilter(typeof(AnonymousOnly))]
        public async Task<IActionResult> Login()
        {
            var providers = (await authenticationSchemeProvider.GetAllSchemesAsync())
                .Select(provider => provider.DisplayName).Where(name => !string.IsNullOrWhiteSpace(name));

            LoginVM loginVM = new LoginVM();
            loginVM.Providers = providers;
            loginVM.LoginUserData = new LoginUserData();

            return View(loginVM);
        }

        [HttpPost, ServiceFilter(typeof(AnonymousOnly))]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            User userProfile = await userManager.FindByEmailAsync(loginVM.LoginUserData.Email);

            if (ModelState.IsValid && userProfile != null)
            {
                var attempt = await signInManager.PasswordSignInAsync(userProfile.UserName, loginVM.LoginUserData.Password, false, false);

                if (attempt.Succeeded)
                {
                    Console.WriteLine(attempt);
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            loginVM.Providers = (await authenticationSchemeProvider.GetAllSchemesAsync())
                .Select(provider => provider.DisplayName).Where(name => !string.IsNullOrWhiteSpace(name));

            return View(loginVM);
        }

        [ServiceFilter(typeof(AnonymousOnly))]
        public async Task<IActionResult> Register()
        {
            var providers = (await authenticationSchemeProvider.GetAllSchemesAsync())
                .Select(provider => provider.DisplayName).Where(name => !string.IsNullOrWhiteSpace(name));

            RegisterVM registerVM = new RegisterVM();
            registerVM.Providers = providers;

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = registerVM.UserData.Email;
                user.FirstName = registerVM.UserData.FirstName;
                user.LastNames = registerVM.UserData.LastNames;
                user.Email = registerVM.UserData.Email;

                await userManager.CreateAsync(user, registerVM.UserData.Password);
                await signInManager.SignInAsync(user, true);

                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [ServiceFilter(typeof(AnonymousOnly))]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignIn(string provider)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/auth/Process" }, provider);
        }

        public async Task<IActionResult> Process()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);

            if (!result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var userProfile = await userManager.FindByEmailAsync(email);

            if(userProfile == null)
            {
                return RedirectToAction("CreatePassword", "Auth");
            }

            await HttpContext.SignOutAsync();
            await signInManager.SignInAsync(userProfile, true);
            return RedirectToAction("Index", "Dashboard");
        }

        [Authorize]
        public async Task<IActionResult> CreatePassword()
        {

            var email = User.Identity.Name;
            var userInfo = await userManager.FindByEmailAsync(email);

            if (userInfo == null)
            {
                User user = new User();
                return View(user);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreatePassword(User user)
        {
            if (!string.IsNullOrEmpty(user.PasswordHash)
                && !string.IsNullOrEmpty(user.FirstName)
                && !string.IsNullOrEmpty(user.LastNames)
                && ModelState.IsValid)
            {
                user.UserName = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                user.Email = user.UserName;
                var result = await userManager.CreateAsync(user, user.PasswordHash);

                if (result.Succeeded)
                {
                    await signInManager.SignOutAsync();
                    await userManager.AddToRoleAsync(user, "customer");
                    await signInManager.SignInAsync(user, true);

                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View();
        }
    }
}
