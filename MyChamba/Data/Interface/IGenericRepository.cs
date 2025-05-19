using System.Linq.Expressions;

namespace MyChamba.Data.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id); // Devuelve null si no se encuentra
        Task<IEnumerable<TEntity>> GetAllAsync(); // Obtiene todos los registros
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate); // Consulta personalizada
        Task AddAsync(TEntity entity); // Agrega un nuevo registro
        void Update(TEntity entity); // Actualiza un registro existente
        void Delete(TEntity entity); // Elimina un registro
    }
}