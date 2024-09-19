using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Repositories.IRepositories;

namespace SignalRChat.API.Users
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataLayer.Models.Users>>> GetUsers()
        {
            return _unitOfWork.UsersRepository.GetAll().ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataLayer.Models.Users>> GetUser(int id)
        {
            var user = _unitOfWork.UsersRepository.GetFirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, DataLayer.Models.Users user)
        {
            if (id != user.Id)
                return BadRequest();

            try
            {
                _unitOfWork.UsersRepository.Update(user);
                 _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataLayer.Models.Users>> PostUser(DataLayer.Models.Users user)
        {
            _unitOfWork.UsersRepository.Add(user);
            _unitOfWork.SaveAsync();

            return CreatedAtAction("GetUsers", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = _unitOfWork.UsersRepository.GetFirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            _unitOfWork.UsersRepository.Delete(user);
            _unitOfWork.SaveAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            var user = _unitOfWork.UsersRepository.GetFirstOrDefault(u => u.Id == id);
            return user != null;
        }
    }
}
