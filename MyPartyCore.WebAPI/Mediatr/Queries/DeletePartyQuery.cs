using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.WebAPI.Mediatr.Queries
{
    public class DeletePartyQuery : IRequest
    {
        public int Id { get; set; }
    }
}
