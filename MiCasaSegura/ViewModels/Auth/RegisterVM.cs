using System;
using System.Collections.Generic;

namespace MiCasaSegura.ViewModels.Auth
{
    public class RegisterVM
    {
        public IEnumerable<string> Providers { get; set; }
        public UserData UserData { get; set; }
    }
}
