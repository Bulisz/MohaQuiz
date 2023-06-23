using AutoMapper;

namespace MohaQuiz.Backend.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //CreateMap<Model, ModelDTO>()
        //    .ForMember(dest => dest.PropertyId, op => op.MapFrom(src => src.Property.Id));
    }
}
