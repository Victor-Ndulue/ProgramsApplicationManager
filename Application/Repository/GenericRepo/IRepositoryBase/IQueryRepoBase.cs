using System.Linq.Expressions;

namespace Application.Repository.GenericRepo.IRepositoryBase;

public interface IQueryRepoBase<T> where T : class
{
    Task<T> FindByKeysAsync(params object[] keyValues);
    IQueryable<T> FindByCondition
        (Expression<Func<T, bool>> expression,
        bool trackChanges);
    IQueryable<T> FindAll(bool trackChanges);
}
