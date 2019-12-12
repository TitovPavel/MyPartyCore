using MediatR;
using MyPartyCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.WebAPI.Mediatr.Queries
{
    public class CreatePartyQuery : IRequest<Party>
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public bool AgeLimit { get; set; }
        public string OwnerId { get; set; }
    }
}
