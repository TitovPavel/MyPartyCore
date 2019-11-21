using MyPartyCore.DB.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyPartyCore.DB.BL
{
    public interface IPartyService
    {
        Party GetPartyByID(int id);
        List<Participant> ListAll();
        IQueryable<Participant> ListAttendent();
        IQueryable<Participant> ListMissed();
        IQueryable<Party> ListOfCurrentParties();
        IQueryable<Party> ListOfPartiesByOwner(string OwnerId);
        void Vote(Participant participant);
        bool ParticipantBelongUser(Participant participant);
        void AddParty(Party party);
        void UpdateParty(Party party);
    }
}