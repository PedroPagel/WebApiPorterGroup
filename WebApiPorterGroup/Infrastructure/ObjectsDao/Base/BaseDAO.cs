using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infrastructure.ObjectsDao.Base
{
    public class BaseDAO
    {
        public readonly WebApiContext _context;

        public BaseDAO(WebApiContext context)
        {
            _context = context;
        }

        public async Task Add<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Remove<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class
        {
            await Task.Run(() => _context.Update(entity));
        }
    }
}
