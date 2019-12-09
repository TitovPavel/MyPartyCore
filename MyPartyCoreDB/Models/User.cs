using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.DB.Models
{
    public class User : IdentityUser
    {
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public FileModel Avatar { get; set; }
        public int? AvatarId { get; set; }
        public List<Party> Parties { get; set; }
        public List<Participant> Participants { get; set; }

    }
}
