using Common.ObjectWrappers.DTOs.Requests.Creation;
using Common.ObjectWrappers.DTOs.Requests.Update;
using Common.PaginationDefiners;
using Infrastructure.IProgramServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class ProgramController : BaseController
{
    private readonly IProgramQueryService _programQuery;
    private readonly IProgramCommandService _commandService;

    public ProgramController(IProgramQueryService programQuery, IProgramCommandService commandService)
    {
        _programQuery = programQuery;
        _commandService = commandService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateProgramAsync(ProgramRequestDto programRequestDto)
    {
        var result = await _commandService.CreateProgramAsync(programRequestDto);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProgramAsync(ProgramUpdateDto programUpdateDto)
    {
        var result = await _commandService.UpdateProgramAsync(programUpdateDto);
        return StatusCode(result.StatusCode, result);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProgramAsync(string programId)
    {
        var result = await _commandService.DeleteProgramByIdAsync(programId);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProgramsAsync(PaginationParams paginationParams)
    {
        var result = await _programQuery.FindAllProgramAsync(paginationParams);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet, Route("get-by-id/{programId}")]
    public async Task<IActionResult> GetProgramByIdAsync([FromQuery] string programId)
    {
        var result = await _programQuery.FindProgramByIdAsync(programId);
        return StatusCode(result.StatusCode, result);
    }
}
