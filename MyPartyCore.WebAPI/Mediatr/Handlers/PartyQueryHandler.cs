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
    public class PartyQueryHandler : IRequestHandler<PartyQuery, Party>, 
        IRequestHandler<CreatePartyQuery, Party>, 
        IRequestHandler<UpdatePartyQuery, Party>, 
        IRequestHandler<DeletePartyQuery>
    {
        
        private readonly MyPartyContext _context;
        private readonly IMapper _mapper;


        public PartyQueryHandler(MyPartyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<Party> Handle(PartyQuery request, CancellationToken cancellationToken)
        {
            Party party = _context.Parties.SingleOrDefault(x => x.Id == request.Id);
            return Task.FromResult(party);
        }

        public async Task<Party> Handle(CreatePartyQuery request, CancellationToken cancellationToken)
        {

            Party party = _mapper.Map<Party>(request);
            _context.Parties.Add(party);

            await _context.SaveChangesAsync();

            return party;
        }

        public async Task<Party> Handle(UpdatePartyQuery request, CancellationToken cancellationToken)
        {
            Party party = _context.Parties.Find(request.Id);

            if (party != null)
            {
                party.Title = request.Title;
                party.Location = request.Location;
                party.Date = request.Date;
                party.AgeLimit = request.AgeLimit;
                _context.Parties.Update(party);
            }

            await _context.SaveChangesAsync();
            return party;
        }

        public async Task<Unit> Handle(DeletePartyQuery request, CancellationToken cancellationToken)
        {
            var person = await _context.Parties.SingleOrDefaultAsync(v => v.Id == request.Id);
            if (person == null)
            {
                throw new Exception("Record does not exist");
            }
            _context.Parties.Remove(person);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
