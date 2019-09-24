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
            bool newParticipant = true;
            bool saveParticipant = true;

            List<Participant> participants = participantsRepository.List();
            foreach (var p in participants)
            {
                if(p.Name == participant.Name)
                {
                    if (p.Attend != participant.Attend)
                    {
                        p.Attend = participant.Attend;
                        p.Reason = participant.Reason;
                        p.ArrivalDate = participant.ArrivalDate;
                    }
                    else
                    {
                        saveParticipant = false;
                    }
                    newParticipant = false;
                    continue;
                }
            }
            if (newParticipant)
            {
                participants.Add(participant);
            }

            if (saveParticipant)
            {
                participantsRepository.Save(participants);
            }

        }

        public List<Participant> ListAll()
        {
            return participantsRepository.List();
        }

        public List<Participant> ListAttendent()
        {
            return participantsRepository.List().Where(p=>p.Attend == true).ToList();
        }
        public List<Participant> ListMissed()
        {
            return participantsRepository.List().Where(p => p.Attend == false).ToList();
        }

        public List<Party> ListOfCurrentParties()
        {
            return partyRepository.List().Where(p => p.Date >= DateTime.Now).ToList();
        }

        public Party GetPartyByID(int id)
        {
            return partyRepository.GetByID(id);
        }
    }
}
