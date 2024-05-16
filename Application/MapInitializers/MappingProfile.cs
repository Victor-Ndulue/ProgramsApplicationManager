using AutoMapper;
using Common.ObjectWrappers.DTOs.Requests.Creation;
using Common.ObjectWrappers.DTOs.Requests.Update;
using Common.ObjectWrappers.DTOs.Responses;
using Domain.Models.Entities;

namespace Application.MappingServices.MapInitializers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProgramRequestDto, Program>()
            .ForMember(prog => prog.Questions,
            config => config.MapFrom(progDto => progDto.questions))
            .ForMember(prog => prog.CustomQuestions,
            config => config.MapFrom(progDto => progDto.customQuestions));
        CreateMap<QuestionRequestDto, Question>();
        CreateMap<CustomQuestionsRequestDto, CustomQuestion>()
            .ForMember(customQues => customQues.Choices,
            config => config.MapFrom(customQuesDto => customQuesDto.choices));
        CreateMap<Program, ProgramResponseDto>()
            .ForMember(progDto => progDto.questions,
            config => config.MapFrom(prog => prog.Questions))
            .ForMember(progDto => progDto.customQuestions,
            config => config.MapFrom(prog => prog.CustomQuestions));
        CreateMap<Question, QuestionResponseDto>();
        CreateMap<CustomQuestion, CustomQuestionResponseDto>()
            .ForMember(cusQuesDto => cusQuesDto.choices,
            config => config.MapFrom(cusQues => cusQues.Choices));
        CreateMap<ChoiceRequestDto, Choice>();
        CreateMap<Choice, ChoiceResponseDto>();
        CreateMap<ProgramUpdateDto, Program>();
        CreateMap<ChoiceUpdateDto, Choice>();
        CreateMap<CustomQuestionUpdateDto, CustomQuestion>();
        CreateMap<QuestionUpdateDto, Question>();
    }
}
