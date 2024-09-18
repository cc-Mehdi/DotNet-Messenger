using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories.IRepositories;

namespace DataLayer.Repositories
{
    public class MessagesRepository : Repository<Messages>, IMessagesRepository
    {
        private readonly ApplicationDbContext _db;
        public MessagesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Update(Messages message)
        {
            try
            {
                var objFromDb = _db.Messages.FirstOrDefault(u => u.Id == message.Id);
                objFromDb.Sender = message.Sender;
                objFromDb.SenderId = message.SenderId;
                objFromDb.MessageContent = message.MessageContent;
                objFromDb.CreateDate = message.CreateDate;

                return true;
            }
            catch { return false; }
        }
    }
}
