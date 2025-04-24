using System.Linq.Expressions;

namespace MyChamba.Data.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(uint id);
        Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity entity);
        void Delete(TEntity entity);
        
        // ✅ NUEVO MÉTODO
        IQueryable<TEntity> GetAllAsQueryable();
    }
}