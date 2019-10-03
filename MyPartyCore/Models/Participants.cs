using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPartyCore.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Attend { get; set; }
        public string Reason { get; set; }
        public string Email { get; set; }
        public DateTime ArrivalDate { get; set; }
        public Party Party { get; set; }
        public int PartyId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
