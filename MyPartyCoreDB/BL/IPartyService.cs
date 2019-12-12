using MyPartyCore.DB.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyPartyCore.DB.BL
{
    public interface IPartyService
    {
        Party GetPartyWithOwnerByID(int id);
        Party GetPartyByID(int id);
        void AddParty(Party party);
        void UpdateParty(Party party);
        void DeleteParty(Party party);
        IQueryable<Party> ListOfCurrentParties();
        IQueryable<Party> ListOfPartiesByOwner(string OwnerId);

        List<Participant> ListAll();
        IQueryable<Participant> ListAttendent();
        IQueryable<Participant> ListMissed();
        void Vote(Participant participant);
        bool ParticipantBelongUser(Participant participant);
        void AddMessageChat(ChatMessage chatMessage);
        IQueryable<ChatMessage> GetChatMessagesByPartyId(int partyId);
    }
}