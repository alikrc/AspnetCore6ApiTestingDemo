using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspnetCore6ApiTestingDemo.Infra;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DemoContext context;
    private readonly DbSet<TEntity> dbSet;

    public Repository(DemoContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetByIdAsync<TId>(TId id) => await dbSet.FindAsync(new object[] { id });

    public virtual async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includes)
    {
        return await GetQueryWithIncludes(predicate, includes).SingleOrDefaultAsync();
    }

    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includes)
    {
        return await GetQueryWithIncludes(predicate, includes).ToListAsync();
    }

    public virtual IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate)
    {
        return dbSet.Where(predicate);
    }

    public virtual async Task<List<TEntity>> ListAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await dbSet.AnyAsync(expression);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);

        return entity;
    }

    public virtual void Update(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
        dbSet.Remove(entity);
    }

    public virtual async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken) > -1;

    private IQueryable<TEntity> GetQueryWithIncludes(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes)
    {
        var query = dbSet.Where(predicate);

        return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}