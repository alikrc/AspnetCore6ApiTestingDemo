using System.Linq.Expressions;

namespace AspnetCore6ApiTestingDemo.Infra;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

    IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate);

    void Delete(TEntity entity);

    Task<TEntity> GetByIdAsync<TId>(TId id);

    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    Task<List<TEntity>> ListAllAsync();

    Task<bool> SaveAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    void Update(TEntity entity);
}