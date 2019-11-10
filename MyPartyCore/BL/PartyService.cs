using Microsoft.EntityFrameworkCore;
using MyPartyCore.DAL;
using MyPartyCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPartyCore.BL
{
    class PartyService : IPartyService
    {

        MyPartyContext _context;

        public PartyService(MyPartyContext context)
        {
            _context = context;
        }

        public void Vote(Participant participant)
        {
            Participant p = _context.Participants.FirstOrDefault(x => x.Name == participant.Name && x.PartyId == participant.PartyId);

            if(p == null)
            {
                _context.Participants.Add(participant);
            }
            else
            {
                p.ArrivalDate = participant.ArrivalDate;
                p.Attend = participant.Attend;
                p.Email = participant.Email;
                p.Reason = participant.Reason;
                _context.Participants.Update(p);
            }
            _context.SaveChanges();
        }

        public bool ParticipantBelongUser(Participant participant)
        {
            Participant p = _context.Participants.FirstOrDefault(x => x.Name == participant.Name && x.PartyId == participant.PartyId);
            if (p != null && p.UserId != participant.UserId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddParty(Party party)
        {
            _context.Parties.Add(party);
            _context.SaveChanges();
        }

        public void UpdateParty(Party party)
        {
            Party updateParty = _context.Parties.Find(party.Id);

            if (updateParty != null)
            {
                updateParty.Title = party.Title;
                updateParty.Location = party.Location;
                updateParty.Date = party.Date;
                updateParty.AgeLimit = party.AgeLimit;
                _context.Parties.Update(updateParty);
            }
            _context.SaveChanges();
        }

        public List<Participant> ListAll()
        {
            return _context.Participants.ToList();
        }

        public IQueryable<Participant> ListAttendent()
        {
            return _context.Participants.Where(p=>p.Attend == true);
        }

        public IQueryable<Participant> ListMissed()
        {
            return _context.Participants.Where(p => p.Attend == false);
        }

        public IQueryable<Party> ListOfCurrentParties()
        {
            return _context.Parties.Where(p => p.Date >= DateTime.Now);
        }

        public IQueryable<Party> ListOfPartiesByOwner(string OwnerId)
        {
            return _context.Parties.Where(p => p.OwnerId == OwnerId);
        }

        public Party GetPartyByID(int id)
        {
            return _context.Parties.Include(i => i.Owner).SingleOrDefault(x => x.Id == id);
        }
    }
}
