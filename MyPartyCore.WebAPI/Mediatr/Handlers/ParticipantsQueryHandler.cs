using MediatR;
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
    public class ParticipantsQueryHandler : IRequestHandler<ParticipantsQuery, IEnumerable<Participant>>
    {
        private readonly MyPartyContext _context;

        public ParticipantsQueryHandler(MyPartyContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Participant>> Handle(ParticipantsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Participant> participants;

            if (request.OnlyAttendent)
            {
                participants = _context.Participants.Where(p => p.Attend == true);
            }
            else
            {
                participants = _context.Participants.ToList();
            }
            return Task.FromResult(participants);
        }
    }
}
