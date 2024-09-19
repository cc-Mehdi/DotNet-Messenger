using DataLayer.Models;
using DataLayer.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace SignalRChat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public List<Messages> messagesList;
        public Users User { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            messagesList = new();
            User = new();
        }

        public void OnGet()
        {
            if (Request.Cookies["UserToken"] != null)
            {
                var user = _unitOfWork.UsersRepository.GetFirstOrDefault(u => u.PublicId == Request.Cookies["UserToken"]);
                if (user != null)
                {
                    User = user;
                    messagesList = _unitOfWork.MessagesRepository.GetAll(new Expression<Func<Messages, object>>[] { m => m.Sender }).ToList();
                    return;
                }
            }
            Response.Redirect("/Customer/Login");
            return;
        }
    }
}
