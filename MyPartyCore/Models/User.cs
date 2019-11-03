using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.Models
{
    public class User : IdentityUser
    {
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }

    }
}
