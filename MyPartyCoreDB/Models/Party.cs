using System;
using System.Collections.Generic;

namespace MyPartyCore.DB.Models
{
    public class Party
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public User Owner { get; set; }
        public string OwnerId { get; set; }
        public List<Participant> Participants { get; set; }
        public bool AgeLimit { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
    }
}