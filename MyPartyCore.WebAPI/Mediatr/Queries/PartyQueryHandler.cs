using MediatR;
using MyPartyCore.DB.BL;
using MyPartyCore.DB.Models;
using MyPartyCore.WebAPI.Mediatr.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPartyCore.WebAPI.Mediatr.Queries
{
    public class PartyQueryHandler : IRequestHandler<PartyQuery, Party>
    {
        private readonly IPartyService _partyService;

        public PartyQueryHandler(IPartyService partyService)
        {
            _partyService = partyService;
        }

        public Task<Party> Handle(PartyQuery request, CancellationToken cancellationToken)
        {
            Party party = _partyService.GetPartyByID(request.Id);
            return Task.FromResult(party);
        }
    }
}
