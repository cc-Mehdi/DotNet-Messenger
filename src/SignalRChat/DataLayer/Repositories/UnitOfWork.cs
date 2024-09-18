using DataLayer.Context;
using DataLayer.Repositories.IRepositories;

namespace DataLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IUsersRepository UsersRepository { get; set; }
        public IMessagesRepository MessagesRepository { get; set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            UsersRepository = new UsersRepository(db);
            MessagesRepository = new MessagesRepository(db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async void SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
