using MyPartyCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyPartyCore.BL
{
    public interface IPartyService
    {
        Party GetPartyByID(int id);
        List<Participant> ListAll();
        IQueryable<Participant> ListAttendent();
        IQueryable<Participant> ListMissed();
        IQueryable<Party> ListOfCurrentParties();
        void Vote(Participant participant);
    }
}