using System.Linq.Expressions;

namespace DataLayer.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public IEnumerable<T> GetByFilter(Expression<Func<T, bool>>? filter = null);
        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null);
        public void Delete(T entity);
        public void Add(T entity);
    }
}
