using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ViewModels
{
    public class PartiesByOwnerViewModel
    {
        public string Id { get; set; }
        public List<PartyViewModel> Parties { get; set; }
    }
}
