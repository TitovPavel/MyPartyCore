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
    }

    public class PartyParticipants
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Время прибытия")]
        [DataType(DataType.Time)]
        public DateTime ArrivalDate { get; set; }
        public int Id { get; set; }

    }
}