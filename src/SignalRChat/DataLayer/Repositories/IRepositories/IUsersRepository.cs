using DataLayer.Models;

namespace DataLayer.Repositories.IRepositories
{
    public interface IUsersRepository : IRepository<Users>
    {
        public bool Update(Users user);
    }
}
