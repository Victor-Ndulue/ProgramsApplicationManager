using Application.Repository.GenericRepo.IRepositoryBase;
using AutoMapper;
using Common.ObjectWrappers.DTOs.Requests.Creation;
using Common.ObjectWrappers.DTOs.Responses;
using Common.ObjectWrappers.ResponseWrapper;
using Domain.Models.Entities;
using Infrastructure.ICandidateApplicationServices;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository.CandidateApplicationServices;

public sealed class CandidateAppCommandServices : ICandidateAppCommandServices
{
    private readonly ICommandRepoBase<CandidateApplication> _applicationCommand;
    private readonly ICommandRepoBase<Answer> _answerCommand;
    private readonly ICommandRepoBase<Choice> _choiceCommand;
    private readonly IQueryRepoBase<Program> _programQuery;
    private readonly IMapper _mapper;

    public CandidateAppCommandServices(ICommandRepoBase<CandidateApplication> applicationCommand, IQueryRepoBase<Program> programQuery, IMapper mapper, ICommandRepoBase<Answer> answerCommand, ICommandRepoBase<Choice> choiceCommand)
    {
        _applicationCommand = applicationCommand;
        _programQuery = programQuery;
        _mapper = mapper;
        _answerCommand = answerCommand;
        _choiceCommand = choiceCommand;
    }
    public async Task<ResponseObject<CandidateAppResponseDto>> ApplyForProgram(CandidateAppRequestDto candidateAppRequest)
    {
        var program = await _programQuery
            .FindByCondition(prog => prog.Id == candidateAppRequest.programId, trackChanges: false)
            .SingleOrDefaultAsync();
        if (program is null)
        {
            var errorMsg = "Program not found";
            return ResponseObject<CandidateAppResponseDto>.FailureResponse(message: errorMsg);
        }
        var candidateApp = _mapper.Map<CandidateApplication>(candidateAppRequest);
        await _answerCommand.CreateManyAsync(candidateApp.Answers);
        foreach (var answer in candidateApp.Answers)
        {
            await _choiceCommand.CreateManyAsync(answer.Choices);
        }
        await _applicationCommand.CreateAsync(candidateApp);
        await _applicationCommand.SaveChangesAsync();
        var responseDto = _mapper.Map<CandidateAppResponseDto>(candidateApp);
        return ResponseObject<CandidateAppResponseDto>.SuccessResponse(data: responseDto);
    }
}
