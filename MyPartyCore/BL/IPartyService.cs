using MyPartyCore.Models;
using System.Collections.Generic;

namespace MyPartyCore.BL
{
    public interface IPartyService
    {
        Party GetPartyByID(int id);
        List<Participant> ListAll();
        List<Participant> ListAttendent();
        List<Participant> ListMissed();
        List<Party> ListOfCurrentParties();
        void Vote(Participant participant);
    }
}