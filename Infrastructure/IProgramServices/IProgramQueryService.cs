using Common.ObjectWrappers.DTOs.Responses;
using Common.ObjectWrappers.ResponseWrapper;
using Common.PaginationDefiners;

namespace Infrastructure.IProgramServices;

public interface IProgramQueryService
{
    Task<ResponseObject<ProgramResponseDto>> FindProgramByIdAsync(string programId);
    Task<ResponseObject<PagedList<ProgramResponseDto>>> FindAllProgramAsync(PaginationParams paginationParams);
}
