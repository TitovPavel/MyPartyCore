using MyPartyCore.DAL;
using MyPartyCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPartyCore.BL
{
    class PartyService : IPartyService
    {
        IParticipantsRepository participantsRepository;
        IPartyRepository partyRepository;

        public PartyService(IParticipantsRepository participantsRepository, IPartyRepository partyRepository)
        {
            this.participantsRepository = participantsRepository;
            this.partyRepository = partyRepository;
        }

        public void Vote(Participant participant)
        {
            List<Participant> participants = participantsRepository.GetAll();

            if(participants.FirstOrDefault(x=>x.Name==participant.Name)==null)
            {
                participantsRepository.Add(participant);
            }
            else
            {
                participantsRepository.Update(participant);
            }
        }

        public List<Participant> ListAll()
        {
            return participantsRepository.GetAll();
        }

        public List<Participant> ListAttendent()
        {
            return participantsRepository.GetAll().Where(p=>p.Attend == true).ToList();
        }
        public List<Participant> ListMissed()
        {
            return participantsRepository.GetAll().Where(p => p.Attend == false).ToList();
        }

        public List<Party> ListOfCurrentParties()
        {
            return partyRepository.GetAll().Where(p => p.Date >= DateTime.Now).ToList();
        }

        public Party GetPartyByID(int id)
        {
            return partyRepository.GetById(id);
        }
    }
}
