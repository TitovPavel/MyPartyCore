using MediatR;
using MyPartyCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.WebAPI.Mediatr.Queries
{
    public class ParticipantsQuery : IRequest<IEnumerable<Participant>>
    {
        public bool OnlyAttendent { get; set; }
    }
}
