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
    public class PartiesQueryHandler : IRequestHandler<PartiesQuery, IEnumerable<Party>>
    {
        private readonly IPartyService _partyService;

        public PartiesQueryHandler(IPartyService partyService)
        {
            _partyService = partyService;
        }

        public Task<IEnumerable<Party>> Handle(PartiesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Party> party = _partyService.ListOfCurrentParties();
            return Task.FromResult(party);
        }
    }
}
