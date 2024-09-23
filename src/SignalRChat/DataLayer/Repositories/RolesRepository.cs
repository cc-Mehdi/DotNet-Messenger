using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories.IRepositories;

namespace DataLayer.Repositories
{
    public class RolesRepository : Repository<Roles>, IRolesRepository
    {
        private readonly ApplicationDbContext _db;
        public RolesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Roles role)
        {
            var objFromDb = _db.Roles.FirstOrDefault(u => u.Id == role.Id);
            objFromDb.PublicId = role.PublicId;
            objFromDb.Title = role.Title;
        }
    }
}
