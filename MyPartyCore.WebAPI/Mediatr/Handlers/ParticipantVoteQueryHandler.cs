using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyPartyCore.DB.BL;
using MyPartyCore.DB.DAL;
using MyPartyCore.DB.Models;
using MyPartyCore.WebAPI.Mediatr.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPartyCore.WebAPI.Mediatr.Handlers
{
    public class ParticipantVoteQueryHandler : IRequestHandler<ParticipantVoteQuery, Participant>
    {
        
        private readonly MyPartyContext _context;
        private readonly IMapper _mapper;


        public ParticipantVoteQueryHandler(MyPartyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Participant> Handle(ParticipantVoteQuery request, CancellationToken cancellationToken)
        {
            Participant participant = _context.Participants.FirstOrDefault(x => x.Name == request.Name && x.PartyId == request.PartyId);

            if (participant == null)
            {
                Participant newParticipant = _mapper.Map<Participant>(request);
                _context.Participants.Add(newParticipant);
            }
            else
            {
                participant.ArrivalDate = request.ArrivalDate;
                participant.Attend = request.Attend;
                participant.Email = request.Email;
                participant.Reason = request.Reason;
                _context.Participants.Update(participant);
            }
            await _context.SaveChangesAsync();
            return participant;
        }
    }
}
