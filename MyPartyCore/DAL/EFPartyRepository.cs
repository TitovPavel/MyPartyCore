using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPartyCore.Models;

namespace MyPartyCore.DAL
{
    public class EFPartyRepository : IPartyRepository
    {
        private readonly MyPartyContext _db;

        public EFPartyRepository(MyPartyContext db)
        {
            _db = db;    
        }

        public void Add(Party party)
        {

            _db.Parties.Add(party);
            _db.SaveChanges();
        }

        public void Delete(Party party)
        {
            _db.Parties.Remove(party);
            _db.SaveChanges();
        }

        public List<Party> GetAll()
        {
            return _db.Parties.ToList<Party>();
        }

        public Party GetById(int partyID)
        {
            return _db.Parties.Find(partyID); 
        }

        public void Update(Party party)
        {
            _db.Parties.Update(party);
            _db.SaveChanges();
        }
}
}
