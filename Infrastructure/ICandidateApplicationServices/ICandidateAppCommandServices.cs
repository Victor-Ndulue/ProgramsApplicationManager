using Common.ObjectWrappers.DTOs.Requests.Creation;
using Common.ObjectWrappers.DTOs.Responses;
using Common.ObjectWrappers.ResponseWrapper;

namespace Infrastructure.ICandidateApplicationServices;

public interface ICandidateAppCommandServices
{
    Task<ResponseObject<CandidateAppResponseDto>> ApplyForProgram(CandidateAppRequestDto candidateAppRequest);
}
