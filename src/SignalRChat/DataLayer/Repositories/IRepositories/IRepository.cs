using System.Linq.Expressions;

namespace DataLayer.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll(Expression<Func<T, object>>[]? includes = null);
        public IEnumerable<T> GetByFilter(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null);
        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null);
        public void Delete(T entity);
        public void Add(T entity);
    }
}
