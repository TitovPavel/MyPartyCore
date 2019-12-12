using MediatR;
using MyPartyCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.WebAPI.Mediatr.Queries
{
    public class ParticipantVoteQuery : IRequest<Participant>
    {
        public string Name { get; set; }
        public bool Attend { get; set; }
        public string Reason { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public int PartyId { get; set; }
        public string UserId { get; set; }
    }
}
