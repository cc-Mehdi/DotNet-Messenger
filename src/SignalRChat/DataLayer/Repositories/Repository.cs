using DataLayer.Context;
using DataLayer.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = _dbSet;

            // Apply the includes if provided
            if(includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.ToList();
        }

        public IEnumerable<T> GetByFilter(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            return query.FirstOrDefault();
        }
    }
}
