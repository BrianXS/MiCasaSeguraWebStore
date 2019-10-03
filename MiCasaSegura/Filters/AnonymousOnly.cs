using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MiCasaSegura.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MiCasaSegura.Filters
{
    public class AnonymousOnly : ActionFilterAttribute
    {
        private readonly UserManager<User> userManager;

        public AnonymousOnly(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var userIsPersisted = UserExists(context.HttpContext.User.Identity.Name);

            try
            {
                Task.WaitAll(userIsPersisted);
                if (context.HttpContext.User.Identity.IsAuthenticated && userIsPersisted.Result == null)
                {
                    context.Result = new RedirectToActionResult("Index", "Dashboard", null);
                }
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae.ToString());
            }

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("CreatePassword", "Auth", null);
            }
        }

        private async Task<string> UserExists(string email)
        {
            var results = await userManager.FindByEmailAsync(email);

            return results.FirstName;
        }
    }

}
