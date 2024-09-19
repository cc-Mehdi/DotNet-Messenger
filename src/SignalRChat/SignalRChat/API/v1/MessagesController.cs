using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Repositories.IRepositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Azure.Core;
using DataLayer.Models;

namespace SignalRChat.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataLayer.Models.Messages>>> GetMessages()
        {
            return _unitOfWork.MessagesRepository.GetAll().ToList();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataLayer.Models.Messages>> GetMessage(int id)
        {
            var message = _unitOfWork.MessagesRepository.GetFirstOrDefault(u => u.Id == id);

            if (message == null)
                return NotFound();

            return message;
        }

        // GET: api/Messages/1234-abcd-1234-abcd
        [HttpGet("GetMessageByPublicId/{publicId}")]
        public async Task<ActionResult<DataLayer.Models.Messages>> GetMessageByPublicId(string publicId)
        {
            var message = _unitOfWork.MessagesRepository.GetFirstOrDefault(u => u.PublicId == publicId);

            if (message == null)
                return NotFound();

            return message;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, DataLayer.Models.Messages message)
        {
            if (id != message.Id)
                return BadRequest();

            try
            {
                _unitOfWork.MessagesRepository.Update(message);
                _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessagesExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataLayer.Models.Messages>> PostMessage([FromBody] UserMessage userMessage)
        {
            if (string.IsNullOrWhiteSpace(userMessage.content))
                return BadRequest("Message content cannot be empty.");

            if (!Request.Cookies.TryGetValue("UserToken", out string userPublicId))
                return BadRequest("UserToken cookie is missing or invalid.");

            var user = _unitOfWork.UsersRepository.GetFirstOrDefault(u => u.PublicId == userPublicId);
            if (user == null)
                return NotFound("User not found.");

            var newMessage = new Messages
            {
                MessageContent = userMessage.content,
                SenderId = user.Id,
                Sender = user
            };

            _unitOfWork.MessagesRepository.Add(newMessage);
            _unitOfWork.Save();

            return CreatedAtAction("GetMessages", new { id = newMessage.Id }, newMessage);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = _unitOfWork.MessagesRepository.GetFirstOrDefault(u => u.Id == id);
            if (message == null)
                return NotFound();

            _unitOfWork.MessagesRepository.Delete(message);
            _unitOfWork.SaveAsync();

            return NoContent();
        }

        private bool MessagesExists(int id)
        {
            var message = _unitOfWork.MessagesRepository.GetFirstOrDefault(u => u.Id == id);
            return message != null;
        }
    }

    public class UserMessage
    {
        public string content { get; set; }
    }
}
