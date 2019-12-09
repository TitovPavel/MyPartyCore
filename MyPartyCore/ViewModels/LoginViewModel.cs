using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
