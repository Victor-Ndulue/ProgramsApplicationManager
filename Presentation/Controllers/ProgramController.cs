using Common.Enums;
using Common.ObjectWrappers.DTOs.Requests.Creation;
using Common.ObjectWrappers.DTOs.Requests.Update;
using Common.PaginationDefiners;
using Infrastructure.IProgramServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class ProgramController : BaseController
{
    private readonly IProgramQueryService _programQuery;
    private readonly IProgramCommandService _programCommand;

    public ProgramController(IProgramQueryService programQuery, IProgramCommandService commandService)
    {
        _programQuery = programQuery;
        _programCommand = commandService;
    }
    [HttpPost, Route("create-program")]
    public async Task<IActionResult> CreateProgramAsync(ProgramRequestDto programRequestDto)
    {
        var result = await _programCommand.CreateProgramAsync(programRequestDto);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut, Route("upddate-program")]
    public async Task<IActionResult> UpdateProgramAsync(ProgramUpdateDto programUpdateDto)
    {
        var result = await _programCommand.UpdateProgramAsync(programUpdateDto);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut, Route("update-question")]
    public async Task<IActionResult> UpdateQuestionAsync(QuestionUpdateDto questionUpdateDto)
    {
        var result = await _programCommand.UpdateQuestionAsync(questionUpdateDto);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut, Route("update-custom-question")]
    public async Task<IActionResult> UpdateCustomQuestionAsync(CustomQuestionUpdateDto customQuestion)
    {
        var result = await _programCommand.UpdateCustomQuestionAsync(customQuestion);
        return StatusCode(result.StatusCode, result);
    }
    [HttpDelete, Route("delete-question")]
    public async Task<IActionResult> DeleteQuestionByIdAsync(string questionId)
    {
        var result = await _programCommand.DeleteQuestionByIdAsync(questionId);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet, Route("get-all-program")]
    public async Task<IActionResult> GetAllProgramsAsync([FromQuery] PaginationParams paginationParams)
    {
        var result = await _programQuery.FindAllProgramAsync(paginationParams);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet, Route("get-program-by-id")]
    public async Task<IActionResult> GetProgramByIdAsync([FromQuery] string programId)
    {
        var result = await _programQuery.FindProgramByIdAsync(programId);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet, Route("get-questions-by-type")]
    public async Task<IActionResult> GetQuestionsByType(QuestionType questionType)
    {
        var result = await _programQuery.GetQuestionsByQuestionType(questionType);
        return StatusCode(result.StatusCode, result);
    }
}
