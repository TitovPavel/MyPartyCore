using MediatR;
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
    public class PartiesQueryHandler : IRequestHandler<PartiesQuery, IEnumerable<Party>>
    {
        private readonly MyPartyContext _context;
        
        public PartiesQueryHandler(MyPartyContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Party>> Handle(PartiesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Party> party = _context.Parties.Where(p => p.Date >= DateTime.Now);
            return Task.FromResult(party);
        }
    }
}
