using Application.Repository.GenericRepo.IRepositoryBase;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Enums;
using Common.ObjectWrappers.DTOs.Responses;
using Common.ObjectWrappers.ResponseWrapper;
using Common.PaginationDefiners;
using Domain.Models.Entities;
using Infrastructure.IProgramServices;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository.ProgramServices;

public sealed class ProgramQueryService : IProgramQueryService
{
    private readonly IQueryRepoBase<Program> _programQuery;
    private readonly IQueryRepoBase<CustomQuestion> _customQuestionQuery;
    private readonly IMapper _mapper;
    private readonly IQueryRepoBase<Answer> _answerQuery;
    public ProgramQueryService(IQueryRepoBase<Program> programQuery, IMapper mapper, IQueryRepoBase<CustomQuestion> customQuestionQuery, IQueryRepoBase<Answer> answerQuery)
    {
        _programQuery = programQuery;
        _mapper = mapper;
        _customQuestionQuery = customQuestionQuery;
        _answerQuery = answerQuery;
    }

    public async Task<ResponseObject<PagedList<ProgramResponseDto>>> FindAllProgramAsync(PaginationParams paginationParams)
    {
        var programQuery = _programQuery.FindAll(trackChanges: false)
            .Include(prog => prog.Questions)
            .Include(prog => prog.CustomQuestions)
            .ThenInclude(customQues => customQues.Choices);
        var programResponse = programQuery.ProjectTo<ProgramResponseDto>(_mapper.ConfigurationProvider);
        var pagedProgramResponse = await PagedList<ProgramResponseDto>.CreateAsync(programResponse, paginationParams.PageNumber, paginationParams.PageSize);
        return ResponseObject<PagedList<ProgramResponseDto>>.SuccessResponse(data: pagedProgramResponse);
    }

    public async Task<ResponseObject<ProgramResponseDto>> FindProgramByIdAsync(string programId)
    {
        var program = await _programQuery
            .FindByCondition(prog => prog.Id == programId, trackChanges: false)
            .Include(prog => prog.Questions)
            .Include(prog => prog.CustomQuestions)
            .ThenInclude(customQues => customQues.Choices)
            .SingleOrDefaultAsync();
        if (program is null)
        {
            var errorMsg = "Not Found";
            return ResponseObject<ProgramResponseDto>.FailureResponse(message: errorMsg);
        }
        var programDto = _mapper.Map<ProgramResponseDto>(program);
        return ResponseObject<ProgramResponseDto>.SuccessResponse(data: programDto);

    }

    public async Task<ResponseObject<IEnumerable<CustomQuestionResponseDto>>> GetQuestionsByQuestionType(QuestionType questionType)
    {
        var questions = await _customQuestionQuery
            .FindByCondition(ques => ques.QuestionType == questionType, trackChanges: false)
            .ToListAsync();
        var response = _mapper.Map<IEnumerable<CustomQuestionResponseDto>>(questions);
        return ResponseObject<IEnumerable<CustomQuestionResponseDto>>.SuccessResponse(data: response);
    }
}
