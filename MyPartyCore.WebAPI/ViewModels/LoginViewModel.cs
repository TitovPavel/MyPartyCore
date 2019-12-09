using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ViewModels
{
    public class LoginViewModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
