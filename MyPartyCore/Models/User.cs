using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}
