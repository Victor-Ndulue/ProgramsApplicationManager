using Application.Repository.GenericRepo.IRepositoryBase;
using AutoMapper;
using Common.ObjectWrappers.DTOs.Responses;
using Common.ObjectWrappers.ResponseWrapper;
using Domain.Models.Entities;
using Infrastructure.ICandidateApplicationServices;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository.CandidateApplicationServices;

public sealed class CandidateAppQueryServices : ICandidateAppQueryServices
{
    private readonly IQueryRepoBase<CandidateApplication> _appQuery;
    private readonly IMapper _mapper;
    public CandidateAppQueryServices(IQueryRepoBase<CandidateApplication> appQuery, IMapper mapper)
    {
        _appQuery = appQuery;
        _mapper = mapper;
    }

    public async Task<ResponseObject<IEnumerable<CandidateAppResponseDto>>> GetApplicationsByProgramId(string programId)
    {
        var applications = await _appQuery
        .FindByCondition(app => app.ProgramId == programId, trackChanges: false)
        .Include(app => app.Answers)
        .ThenInclude(ans => ans.Choices).ToListAsync();
        var response = _mapper.Map<IEnumerable<CandidateAppResponseDto>>(applications);
        return ResponseObject<IEnumerable<CandidateAppResponseDto>>.SuccessResponse(data: response);
    }
}
