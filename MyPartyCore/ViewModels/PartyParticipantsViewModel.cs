using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPartyCore.ViewModels
{
    public class PartyParticipantsViewModel
    {
        public List<PartyParticipants> PartyParticipants { get; set; }
        public string PartyTitle { get; set; }
        public int PartyID { get; set; }
        public PageViewModel PageViewModel { get; set; }

    }

    public class PartyParticipants
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "ArrivalDate")]
        [DataType(DataType.Time)]
        public DateTime ArrivalDate { get; set; }
        public int Id { get; set; }

    }
}