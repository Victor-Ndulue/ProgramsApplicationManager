using Common.ObjectWrappers.DTOs.Requests.Creation;
using Common.ObjectWrappers.DTOs.Requests.Update;
using Common.ObjectWrappers.DTOs.Responses;
using Common.ObjectWrappers.ResponseWrapper;

namespace Infrastructure.IProgramServices;

public interface IProgramCommandService
{
    Task<ResponseObject<ProgramResponseDto>> CreateProgramAsync(ProgramRequestDto programRequestDto);
    Task<ResponseObject<string>> DeleteProgramByIdAsync(string programId);
    Task<ResponseObject<ProgramResponseDto>> UpdateProgramAsync(ProgramUpdateDto programUpdateDto);
}
