using System;
using System.Threading.Tasks;
using MiCasaSegura.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MiCasaSegura.Filters
{
    public class MissingUserInfo : ActionFilterAttribute
    {
        private readonly UserManager<User> userManager;

        public MissingUserInfo(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var result = UserExists(context).Result;
            if (result == null)
            {
                context.Result = new RedirectToActionResult("CreatePassword", "Auth", null);
            }
        }

        public async Task<User> UserExists(ActionExecutingContext context)
        {
            var returnValue = await userManager.FindByEmailAsync(context.HttpContext.User.Identity.Name);
            return returnValue;
        }
    }
}
