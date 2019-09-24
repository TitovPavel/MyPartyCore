using MyPartyCore.Models;
using System.Collections.Generic;

namespace MyPartyCore.DAL
{
    interface IParticipantsRepository
    {
        void Delete(string name);
        List<Participant> List();
        void Save(List<Participant> p);
    }
}