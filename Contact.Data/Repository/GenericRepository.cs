using System.Linq.Expressions;
using Contact.Data.Variables;
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

    public virtual async Task<IEnumerable<T>> GetAllAsync(int page, int pageSize,
        params Expression<Func<T, object>>[]? includes)
    {
        page = Math.Max(page, PagedOptions.Page);
        pageSize = Math.Max(pageSize, PagedOptions.Page);
        
        IQueryable<T> query = DbSet;

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
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

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null,
        CancellationToken token = default)
    {
        if (predicate is not null)
        {
            return await DbSet.CountAsync(predicate, token);
        }

        return await DbSet.CountAsync(token);
    }
}