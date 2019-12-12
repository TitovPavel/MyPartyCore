using System;
using System.Collections.Generic;
using System.Text;

namespace MyPartyCore.DB.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Party Party { get; set; }
        public int PartyId { get; set; }
    }
}
