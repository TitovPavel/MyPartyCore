using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyPartyCore.DB.Models;
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
            CreateMap<EditPartyViewModel, Party>().ReverseMap();
            CreateMap<CreatePartyViewModel, Party>().ReverseMap();
            CreateMap<Participant, PartyParticipants>().ReverseMap();
            CreateMap<RegisterViewModel, User>().ReverseMap();
            CreateMap<CreateUserViewModel, User>().ReverseMap();
            CreateMap<EditUserViewModel, User>().ReverseMap();
            CreateMap<User, UserViewModel>()
                .ForMember(d => d.IsLocked, o => o.MapFrom(s => (s.LockoutEnabled && s.LockoutEnd > DateTime.Now)));
            CreateMap<ProfileSettingsViewModel, User>();
            CreateMap<User, ProfileSettingsViewModel>()
                .ForMember(d => d.AvatarExist, o => o.MapFrom(s => (s.AvatarId!=null)));
            CreateMap<ProfileViewModel, User>();
            CreateMap<User, ProfileViewModel>()
                .ForMember(d => d.AvatarExist, o => o.MapFrom(s => (s.AvatarId != null)));
            CreateMap<User, ChangeRoleViewModel>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id));
        }
    }
}
