using System;
using System.Collections.Generic;

namespace MyPartyCore.Models
{
    public class Party
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Participant> Participants { get; set; }

    }
}