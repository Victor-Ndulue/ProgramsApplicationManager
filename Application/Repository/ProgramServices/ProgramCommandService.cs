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
    private readonly ICommandRepoBase<CustomQuestion> _customQuestionCommand;
    private readonly ICommandRepoBase<Choice> _choiceCommand;

    public ProgramCommandService(ICommandRepoBase<Program> programCommand, IQueryRepoBase<Program> programQuery, IMapper mapper, ICommandRepoBase<Question> questionCommand, ICommandRepoBase<CustomQuestion> customQuestionCommand, ICommandRepoBase<Choice> choiceCommand)
    {
        _programCommand = programCommand;
        _ProgramQuery = programQuery;
        _mapper = mapper;
        _questionCommand = questionCommand;
        _customQuestionCommand = customQuestionCommand;
        _choiceCommand = choiceCommand;
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

    public async Task<ResponseObject<string>> DeleteProgramByIdAsync(string programId)
    {
        var programToUpdate = await GetProgramById(programId);
        if (programToUpdate is null)
        {
            var errorMsg = "Program not found";
            return ResponseObject<string>.FailureResponse(message: errorMsg);
        }
        var successMsg = "Delete successful";
        return ResponseObject<string>.SuccessResponse(data: successMsg);
    }
    public async Task<ResponseObject<ProgramResponseDto>> UpdateProgramAsync(ProgramUpdateDto programUpdateDto)
    {
        var programToUpdate = await _ProgramQuery
            .FindByCondition(prog => prog.Id == programUpdateDto.id, trackChanges: true)
            .Include(prog => prog.Questions)
            .Include(prog => prog.CustomQuestions)
            .ThenInclude(customQues => customQues.Choices)
            .SingleOrDefaultAsync();

        if (programToUpdate is null)
        {
            var errorMsg = "Program not found";
            return ResponseObject<ProgramResponseDto>.FailureResponse(message: errorMsg);
        }

        _mapper.Map(programUpdateDto, programToUpdate);

        // Update Questions
        foreach (var questionDto in programUpdateDto.questions)
        {
            var question = programToUpdate.Questions.FirstOrDefault(q => q.Id == questionDto.id);
            if (question is null)
            {
                var newQuestion = _mapper.Map<Question>(questionDto);
                programToUpdate.Questions.Add(newQuestion);
            }
            else
            {
                _mapper.Map(questionDto, question);
            }
        }

        // Update Custom Questions and Choices
        foreach (var customQuestionDto in programUpdateDto.customQuestions)
        {
            var customQuestion = programToUpdate.CustomQuestions.FirstOrDefault(cq => cq.Id == customQuestionDto.id);
            if (customQuestion is null)
            {
                var newCustomQuestion = _mapper.Map<CustomQuestion>(customQuestionDto);
                programToUpdate.CustomQuestions.Add(newCustomQuestion);
            }
            else
            {
                _mapper.Map(customQuestionDto, customQuestion);

                if (customQuestion.QuestionType == QuestionType.MultipleChoice)
                {
                    foreach (var choiceDto in customQuestionDto.choices)
                    {
                        var choice = customQuestion.Choices.FirstOrDefault(c => c.Id == choiceDto.id);
                        if (choice is null)
                        {
                            var newChoice = _mapper.Map<Choice>(choiceDto);
                            customQuestion.Choices.Add(newChoice);
                        }
                        else
                        {
                            _mapper.Map(choiceDto, choice);
                        }
                    }
                }
            }
        }

        try
        {
            await _programCommand.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Handle concurrency exceptions if needed
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions if needed
            throw;
        }

        var programResponse = _mapper.Map<ProgramResponseDto>(programToUpdate);
        return ResponseObject<ProgramResponseDto>.SuccessResponse(data: programResponse);
    }

    /* public async Task<ResponseObject<ProgramResponseDto>> UpdateProgramAsync(ProgramUpdateDto programUpdateDto)
     {
         var programToUpdate = await _ProgramQuery
             .FindByCondition(prog => prog.Id == programUpdateDto.id, trackChanges: true)
             .Include(prog => prog.Questions)
             .Include(prog => prog.CustomQuestions)
             .ThenInclude(customQues => customQues.Choices)
             .SingleOrDefaultAsync();
         if (programToUpdate is null)
         {
             var errorMsg = "Program not found";
             return ResponseObject<ProgramResponseDto>.FailureResponse(message: errorMsg);
         }
         _mapper.Map(programUpdateDto, programToUpdate);
         // Update Questions
         foreach (var questionDto in programUpdateDto.questions)
         {
             var question = programToUpdate.Questions.FirstOrDefault(q => q.Id == questionDto.id);
             if (question is null)
             {
                 var newQuestion = _mapper.Map<Question>(questionDto);
                 programToUpdate.Questions.Add(newQuestion);
                 //newQuestion.ProgramId = programToUpdate.Id;
                 //await _questionCommand.CreateAsync(newQuestion);
             }
             else
             {
                 _mapper.Map(questionDto, question);
             }
         }


         if (programUpdateDto.customQuestions.Count > 0)
         {
             foreach (var customQuestionDto in programUpdateDto.customQuestions)
             {
                 var customQuestion = programToUpdate.CustomQuestions
                     .FirstOrDefault(cq => cq.Id == customQuestionDto.id);
                 if (customQuestion is null)
                 {
                     var newCustomQuestion = _mapper.Map<CustomQuestion>(customQuestionDto);
                     programToUpdate.CustomQuestions.Add(newCustomQuestion);
                 }
                 else
                 {
                     _mapper.Map(customQuestionDto, customQuestion);

                     if (customQuestion.QuestionType == QuestionType.MultipleChoice)
                     {
                         foreach (var choiceDto in customQuestionDto.choices)
                         {
                             var choice = customQuestion.Choices.FirstOrDefault(c => c.Id == choiceDto.id);
                             if (choice is null)
                             {
                                 var newChoice = _mapper.Map<Choice>(choiceDto);
                                 customQuestion.Choices.Add(newChoice);
                             }
                             else
                             {
                                 _mapper.Map(choiceDto, choice);
                             }
                         }
                     }
                 }
             }
         }
         await _programCommand.SaveChangesAsync();
         var programResponse = _mapper.Map<ProgramResponseDto>(programToUpdate);
         return ResponseObject<ProgramResponseDto>.SuccessResponse(data: programResponse);
     }*/
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
