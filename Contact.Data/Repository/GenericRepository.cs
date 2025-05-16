using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Contact.Data.Repository;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(DbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await DbSet.FindAsync([id], token);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
        CancellationToken token = default, params Expression<Func<T, object>>[]? includes)
    {
        IQueryable<T> query = DbSet;

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(predicate, token);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[]? includes)
    {
        IQueryable<T> query = DbSet;

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }

    public virtual async Task AddAsync(T entity, CancellationToken token = default)
    {
        await DbSet.AddAsync(entity, token);
    }

    public virtual T Update(T entity)
    {
        DbSet.Update(entity);
        return entity;
    }

    public virtual bool Delete(T entity)
    {
        DbSet.Remove(entity);
        return true;
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
    {
        return await DbSet.AnyAsync(predicate, token);
    }
}