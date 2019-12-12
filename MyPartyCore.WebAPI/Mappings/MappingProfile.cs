using AutoMapper;
using MyPartyCore.DB.Models;
using MyPartyCore.WebAPI.Mediatr.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.WebAPI.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Party, CreatePartyQuery>();
            CreateMap<CreatePartyQuery, Party>();
            CreateMap<Participant, ParticipantVoteQuery>();
            CreateMap<ParticipantVoteQuery, Participant>();
        }
    }
}
