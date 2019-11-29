using MediatR;
using MyPartyCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.WebAPI.Mediatr.Handlers
{
    public class PartiesQuery : IRequest<IEnumerable<Party>>
    {
    
    }
}
