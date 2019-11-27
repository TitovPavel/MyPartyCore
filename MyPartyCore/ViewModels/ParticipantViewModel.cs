using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPartyCore.ViewModels
{
    public class ParticipantViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Attend")]
        public bool Attend { get; set; }
        [Display(Name = "Reason")]
        public string Reason { get; set; }
        [Display(Name = "ArrivalDate")]
        [DataType(DataType.Time)]
        public DateTime ArrivalDate { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public int PartyId { get; set; }
        public string UserId { get; set; }
    }
}