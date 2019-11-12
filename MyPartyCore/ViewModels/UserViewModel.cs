using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

         public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public string Sex { get; set; }

        public bool IsLocked { get; set; }
    }
}
