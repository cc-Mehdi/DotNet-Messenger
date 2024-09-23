using DataLayer.Models;

namespace DataLayer.Repositories.IRepositories
{
    public interface IRolesRepository : IRepository<Roles>
    {
        public void Update(Roles role);
    }
}
