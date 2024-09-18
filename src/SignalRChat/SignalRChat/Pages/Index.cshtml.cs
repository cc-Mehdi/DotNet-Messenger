using DataLayer.Models;
using DataLayer.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace SignalRChat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<Messages> messagesList;

        public IndexModel(ILogger<IndexModel> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            messagesList = _unitOfWork.MessagesRepository.GetAll(new Expression<Func<Messages, object>>[] { m => m.Sender });
        }
    }
}
