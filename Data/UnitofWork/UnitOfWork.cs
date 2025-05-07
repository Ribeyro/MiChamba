using System.Collections;
using MyChamba.Data.Context;
using MyChamba.Data.Interface;
using MyChamba.Data.Repository;

namespace MyChamba.Data.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyChambaContext _context;
        private readonly Hashtable _repositories;

        public UnitOfWork(MyChambaContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var typeName = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(typeName))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(typeName, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[typeName];
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