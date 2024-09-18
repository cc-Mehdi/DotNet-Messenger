using DataLayer.Models;

namespace DataLayer.Repositories.IRepositories
{
    public interface IMessagesRepository : IRepository<Messages>
    {
        public void Update(Messages message);
    }
}
