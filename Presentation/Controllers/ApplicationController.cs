using Common.ObjectWrappers.DTOs.Requests.Creation;
using Infrastructure.ICandidateApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class ApplicationController : BaseController
{
    private readonly ICandidateAppCommandServices _candidateAppCommandServices;
    private readonly ICandidateAppQueryServices _candidateAppQueryServices;

    public ApplicationController(ICandidateAppCommandServices candidateAppCommandServices, ICandidateAppQueryServices candidateAppQueryServices)
    {
        _candidateAppCommandServices = candidateAppCommandServices;
        _candidateAppQueryServices = candidateAppQueryServices;
    }
    [HttpPost]
    public async Task<IActionResult> ApplyForProgram(CandidateAppRequestDto candidateAppRequestDto)
    {
        var result = await _candidateAppCommandServices.ApplyForProgram(candidateAppRequestDto);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet("get-program-applications")]
    public async Task<IActionResult> GetProgramApplicationAsync([FromQuery] string programId)
    {
        var result = await _candidateAppQueryServices.GetApplicationsByProgramId(programId);
        return StatusCode(result.StatusCode, result);
    }
}
