using Common.ObjectWrappers.DTOs.Requests.Creation;
using Common.ObjectWrappers.DTOs.Requests.Update;
using Common.ObjectWrappers.DTOs.Responses;
using Common.ObjectWrappers.ResponseWrapper;

namespace Infrastructure.IProgramServices;

public interface IProgramCommandService
{
    Task<ResponseObject<ProgramResponseDto>> CreateProgramAsync(ProgramRequestDto programRequestDto);
    Task<ResponseObject<string>> DeleteQuestionByIdAsync(string programId);
    Task<ResponseObject<ProgramResponseDto>> UpdateProgramAsync(ProgramUpdateDto programUpdateDto);
    Task<ResponseObject<QuestionResponseDto>> UpdateQuestionAsync(QuestionUpdateDto questionUpdateDto);
    Task<ResponseObject<CustomQuestionResponseDto>> UpdateCustomQuestionAsync(CustomQuestionUpdateDto customQuesUpdateDto);
}
