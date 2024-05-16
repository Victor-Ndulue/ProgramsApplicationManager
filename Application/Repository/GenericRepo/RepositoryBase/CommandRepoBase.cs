using Application.DataContext;
using Application.Repository.GenericRepo.IRepositoryBase;

namespace Application.Repository.GenericRepo.RepositoryBase;

//Class used to set generic methods for CRUD operations 

public sealed class CommandRepoBase<T> : ICommandRepoBase<T> where T : class
{
    private readonly ProgramAppDbContext _context;
    //--Implementing Dependency Injection--
    public CommandRepoBase(ProgramAppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }
    public async Task CreateManyAsync(IEnumerable<T> entity)
    {
        await _context.Set<T>().AddRangeAsync(entity);
    }

    public Task UpdateManyAsync(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
        return Task.CompletedTask;
    }
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
