using DataLayer.Models;
using DataLayer.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SignalRChat.Pages.Customer
{
    public class LoginModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Users User { get; set; }

        public LoginModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            User = new Users();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState["User.Username"].ValidationState = ModelValidationState.Valid;
            ModelState["User.Roles"].ValidationState = ModelValidationState.Valid;

            if (!ModelState.IsValid)
                return Page();

            try
            {
                User = _unitOfWork.UsersRepository.GetFirstOrDefault(u => u.Email == User.Email && u.Password == User.Password);
                if (User != null)
                {
                    var cookieOptions = new CookieOptions()
                    {
                        Expires = DateTime.Now.AddMinutes(20),
                        IsEssential = true,
                        HttpOnly = true,
                        Secure = true
                    };
                    Response.Cookies.Append("UserToken", User.PublicId, cookieOptions);

                    // Success
                    ViewData["Notif"] = $"Welcome {User.Username}";
                    return RedirectToPage("./../Index");
                }
                else
                {
                    ViewData["Notif"] = "Email or Password is incorrect!";
                    return Page();
                }
            }
            catch
            {
                // Faild
                ViewData["Notif"] = "Ops... Something was wrong!";
                return Page();
            }
        }

    }
}
