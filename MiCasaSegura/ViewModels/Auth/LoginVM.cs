using System;
using System.Collections.Generic;

namespace MiCasaSegura.ViewModels.Auth
{
    public class LoginVM
    {
        public IEnumerable<string> Providers { get; set; }
        public LoginUserData LoginUserData { get; set; }
    }
}