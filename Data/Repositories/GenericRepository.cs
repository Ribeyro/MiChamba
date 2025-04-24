using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyChamba.Data.Interface;

namespace MyChamba.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly MyChambaContext _context;

        public GenericRepository(MyChambaContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(uint id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with ID {id} was not found.");
            }
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties = "")
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var includeProperty in includeProperties.Split(
                         new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }
        
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }


        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        
        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
        
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }


    }
}