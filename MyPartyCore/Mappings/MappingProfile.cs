using AutoMapper;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;


namespace MyPartyCore.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Participant, ParticipantViewModel>();
            CreateMap<ParticipantViewModel, Participant>()
                .ForMember(d => d.Reason, o => o.MapFrom(s => s.Reason ?? ""));
            CreateMap<Party, PartyViewModel>();
            CreateMap<PartyViewModel, Party>();
            CreateMap<Participant, PartyParticipants>();
            CreateMap<PartyParticipants, Participant>();
        }
    }
}
