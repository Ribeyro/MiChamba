using System.Collections;
using MyChamba.Data.Interface;
using MyChamba.Data.Repository;

namespace MyChamba.Data.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly MyChambaContext _context;

        public UnitOfWork(MyChambaContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var typeName = typeof(TEntity).Name;

            if (_repositories.ContainsKey(typeName))
            {
                return (IGenericRepository<TEntity>)_repositories[typeName];
            }

            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            if (repositoryInstance != null)
            {
                _repositories.Add(typeName, repositoryInstance);
                return (IGenericRepository<TEntity>)repositoryInstance;
            }

            throw new Exception($"Could not create repository instance for type {typeName}");
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}