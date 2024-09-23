using DataLayer.Models;

namespace DataLayer.Repositories.IRepositories
{
    public interface IUsersRepository : IRepository<Users>
    {
        public void Update(Users user);
        public void SetRole(Users user, string roleTitle);
    }
}
