using Common.ObjectWrappers.DTOs.Responses;
using Common.ObjectWrappers.ResponseWrapper;

namespace Infrastructure.ICandidateApplicationServices;

public interface ICandidateAppQueryServices
{
    Task<ResponseObject<IEnumerable<CandidateAppResponseDto>>> GetApplicationsByProgramId(string programId);
}
