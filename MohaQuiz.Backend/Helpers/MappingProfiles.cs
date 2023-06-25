using AutoMapper;
using MohaQuiz.Backend.Models;
using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //CreateMap<Model, ModelDTO>()
        //    .ForMember(dest => dest.PropertyId, op => op.MapFrom(src => src.Property.Id));

        CreateMap<Team, TeamNameDTO>();
        CreateMap<TeamNameDTO, Team>();
        CreateMap<Round, RoundDetailsDTO>();
        CreateMap<RoundType, RoundTypeDTO>();
        CreateMap<Question, QuestionDTO>();
        CreateMap<CorrectAnswer, CorrectAnswerDTO>();
    }
}
