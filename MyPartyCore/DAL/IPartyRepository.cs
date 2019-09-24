using System.Collections.Generic;
using MyPartyCore.Models;

namespace MyPartyCore.DAL
{
    interface IPartyRepository
    {
        void Delete(Party party);
        List<Party> List();
        void Save(List<Party> p);
        Party GetByID(int id);
    }
}
