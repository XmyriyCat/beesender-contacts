using System.Linq.Expressions;

namespace Contact.Data.Repository;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default,
        params Expression<Func<T, object>>[]? includes);

    Task<IEnumerable<T>> GetAllAsync(int page, int pageSize, params Expression<Func<T, object>>[]? includes);

    Task AddAsync(T entity, CancellationToken token = default);

    T Update(T entity);

    bool Delete(T entity);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
    
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken token = default);
}