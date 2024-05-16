using Application.Repository.GenericRepo.IRepositoryBase;
using AutoMapper;
using Common.Enums;
using Common.ObjectWrappers.DTOs.Requests.Creation;
using Common.ObjectWrappers.DTOs.Requests.Update;
using Common.ObjectWrappers.DTOs.Responses;
using Common.ObjectWrappers.ResponseWrapper;
using Domain.Models.Entities;
using Infrastructure.IProgramServices;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository.ProgramServices;

public sealed class ProgramCommandService : IProgramCommandService
{
    private readonly ICommandRepoBase<Program> _programCommand;
    private readonly IQueryRepoBase<Program> _ProgramQuery;
    private readonly IMapper _mapper;
    private readonly ICommandRepoBase<Question> _questionCommand;
    private readonly IQueryRepoBase<Question> _questionQuery;
    private readonly ICommandRepoBase<CustomQuestion> _customQuestionCommand;
    private readonly IQueryRepoBase<CustomQuestion> _customQuestionQuery;
    private readonly ICommandRepoBase<Choice> _choiceCommand;

    public ProgramCommandService(ICommandRepoBase<Program> programCommand, IQueryRepoBase<Program> programQuery, IMapper mapper, ICommandRepoBase<Question> questionCommand, ICommandRepoBase<CustomQuestion> customQuestionCommand, ICommandRepoBase<Choice> choiceCommand, IQueryRepoBase<Question> questionQuery, IQueryRepoBase<CustomQuestion> customQuestionQuery)
    {
        _programCommand = programCommand;
        _ProgramQuery = programQuery;
        _mapper = mapper;
        _questionCommand = questionCommand;
        _customQuestionCommand = customQuestionCommand;
        _choiceCommand = choiceCommand;
        _questionQuery = questionQuery;
        _customQuestionQuery = customQuestionQuery;
    }

    public async Task<ResponseObject<ProgramResponseDto>> CreateProgramAsync(ProgramRequestDto programRequestDto)
    {
        var program = _mapper.Map<Program>(programRequestDto);
        await _questionCommand.CreateManyAsync(program.Questions);
        var checkForCustomQuestion = CheckForCustomQuestion(programRequestDto.customQuestions);
        if (checkForCustomQuestion)
        {
            await _customQuestionCommand.CreateManyAsync(program.CustomQuestions);

            foreach (var question in program.CustomQuestions)
            {
                if (question.QuestionType == QuestionType.MultipleChoice)
                {
                    //question.Choices = question.Choices;
                    await _choiceCommand.CreateManyAsync(question.Choices);
                }
            }
        }
        await _programCommand.CreateAsync(program);
        await _programCommand.SaveChangesAsync();
        var responseDto = _mapper.Map<ProgramResponseDto>(program);
        return ResponseObject<ProgramResponseDto>.SuccessResponse(data: responseDto, statusCode: 201);
    }

    public async Task<ResponseObject<string>> DeleteQuestionByIdAsync(string questionId)
    {
        var question = await _questionQuery.FindByCondition(ques => ques.Id == questionId, trackChanges: true).SingleOrDefaultAsync();
        var customQues = await _customQuestionQuery.FindByCondition(ques => ques.Id == questionId, trackChanges: true).SingleOrDefaultAsync();



        if (question is null && customQues is null)
        {
            var errorMsg = "Question not found";
            return ResponseObject<string>.FailureResponse(message: errorMsg);
        }
        if (question is null)
        {
            _customQuestionCommand.Delete(customQues);
        }
        else
        {
            _questionCommand.Delete(question);
        }
        await _questionCommand.SaveChangesAsync();
        var successMsg = "Delete successful";
        return ResponseObject<string>.SuccessResponse(data: successMsg);
    }
    public async Task<ResponseObject<ProgramResponseDto>> UpdateProgramAsync(ProgramUpdateDto programUpdateDto)
    {
        var programToUpdate = await GetProgramById(programUpdateDto.id);

        if (programToUpdate is null)
        {
            var errorMsg = "Program not found";
            return ResponseObject<ProgramResponseDto>.FailureResponse(message: errorMsg);
        }

        _mapper.Map(programUpdateDto, programToUpdate);
        await _programCommand.SaveChangesAsync();
        var programResponse = _mapper.Map<ProgramResponseDto>(programToUpdate);
        return ResponseObject<ProgramResponseDto>.SuccessResponse(data: programResponse);
    }
    public async Task<ResponseObject<QuestionResponseDto>> UpdateQuestionAsync(QuestionUpdateDto questionUpdateDto)
    {
        var questionToUpdate = await _questionQuery
            .FindByCondition(ques => ques.Id == questionUpdateDto.id, trackChanges: true)
            .SingleOrDefaultAsync();
        if (questionUpdateDto is null)
        {
            var errorMsg = "Question not found";
            return ResponseObject<QuestionResponseDto>.FailureResponse(message: errorMsg);
        }
        _mapper.Map(questionUpdateDto, questionToUpdate);
        await _questionCommand.SaveChangesAsync();
        var response = _mapper.Map<QuestionResponseDto>(questionToUpdate);
        return ResponseObject<QuestionResponseDto>.SuccessResponse(data: response);
    }
    public async Task<ResponseObject<CustomQuestionResponseDto>> UpdateCustomQuestionAsync(CustomQuestionUpdateDto customQuesUpdateDto)
    {
        var questionToUpdate = await _customQuestionQuery
            .FindByCondition(ques => ques.Id == customQuesUpdateDto.id, trackChanges: true)
            .SingleOrDefaultAsync();
        if (questionToUpdate is null)
        {
            var errorMsg = "Question not found";
            return ResponseObject<CustomQuestionResponseDto>.FailureResponse(message: errorMsg);
        }
        _mapper.Map(customQuesUpdateDto, questionToUpdate);

        if (questionToUpdate.QuestionType == QuestionType.MultipleChoice)
        {
            await _choiceCommand.UpdateManyAsync(questionToUpdate.Choices);
        }
        await _questionCommand.SaveChangesAsync();
        var response = _mapper.Map<CustomQuestionResponseDto>(questionToUpdate);
        return ResponseObject<CustomQuestionResponseDto>.SuccessResponse(data: response);
    }
    private async Task<Program> GetProgramById(string programId)
    {
        return await _ProgramQuery
            .FindByCondition(prog => prog.Id == programId, trackChanges: false)
            .Include(prog => prog.Questions)
            .Include(prog => prog.CustomQuestions)
            .ThenInclude(customQues => customQues.Choices)
            .SingleOrDefaultAsync();
    }
    private bool CheckForCustomQuestion(IEnumerable<CustomQuestionsRequestDto> customQuestions)
    {
        return customQuestions.Any();
    }
}
