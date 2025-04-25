using MyChamba.Data.Interface;

namespace MyChamba.Data.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        
        
        Task<int> Complete();
    }
}