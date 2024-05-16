namespace Application.Repository.GenericRepo.IRepositoryBase;

public interface ICommandRepoBase<T> where T : class
{
    Task CreateAsync(T entity);
    Task CreateManyAsync(IEnumerable<T> entity);
    Task UpdateManyAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}
