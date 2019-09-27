using System;
using Microsoft.AspNetCore.Identity;

namespace MiCasaSegura.Models.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastNames { get; set; }
        public string Address { get; set; }
    }
}
