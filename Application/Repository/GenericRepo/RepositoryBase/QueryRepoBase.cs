using Application.DataContext;
using Application.Repository.GenericRepo.IRepositoryBase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Repository.GenericRepo.RepositoryBase;

public sealed class QueryRepoBase<T> : IQueryRepoBase<T> where T : class
{
    private readonly ProgramAppDbContext _context;

    public QueryRepoBase(ProgramAppDbContext context)
    {
        _context = context;
    }

    public async Task<T> FindByKeysAsync(params object[] keyValues)
    {
        return await _context.Set<T>().FindAsync(keyValues);
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return trackChanges ? _context.Set<T>().Where(expression) :
            _context.Set<T>().Where(expression).AsNoTracking();
    }


    public IQueryable<T> FindAll(bool trackChanges)
    {
        return trackChanges ? _context.Set<T>() :
            _context.Set<T>().AsNoTracking();
    }
}
