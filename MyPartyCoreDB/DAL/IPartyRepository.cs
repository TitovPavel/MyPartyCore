using System.Collections.Generic;
using MyPartyCore.DB.Models;

namespace MyPartyCore.DB.DAL
{
    interface IPartyRepository
    {
        List<Party> GetAll();
        Party GetById(int partyID);
        void Add(Party party);
        void Update(Party party);
        void Delete(Party party);
    }
}
