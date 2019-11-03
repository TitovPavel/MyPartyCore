using AutoMapper;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;
using System;

namespace MyPartyCore.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Participant, ParticipantViewModel>();
            CreateMap<ParticipantViewModel, Participant>()
                .ForMember(d => d.Reason, o => o.MapFrom(s => s.Reason ?? ""));
            CreateMap<PartyViewModel, Party>().ReverseMap();
            CreateMap<Participant, PartyParticipants>().ReverseMap();
            CreateMap<RegisterViewModel, User>().ReverseMap();
            CreateMap<CreateUserViewModel, User>().ReverseMap();
            CreateMap<EditUserViewModel, User>().ReverseMap();
            CreateMap<User, UserViewModel>().ForMember(d => d.IsLocked, o => o.MapFrom(s => (s.LockoutEnabled && s.LockoutEnd > DateTime.Now)));
        }
    }
}
