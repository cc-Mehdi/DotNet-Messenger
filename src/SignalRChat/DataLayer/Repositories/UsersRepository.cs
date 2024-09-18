using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories.IRepositories;

namespace DataLayer.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        private readonly ApplicationDbContext _db;
        public UsersRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public bool Update(Users user)
        {
            try
            {
                var objFromDb = _db.Users.FirstOrDefault(u => u.Id == user.Id);
                objFromDb.Username = user.Username;
                objFromDb.Email = user.Email;
                objFromDb.Password = user.Password;
                return true;
            }
            catch { return false; }
        }
    }
}
