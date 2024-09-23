using DataLayer.Models;
using DataLayer.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DataLayer.Context;

namespace SignalRChat.Pages.Customer
{
    public class RegisterModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Users User { get; set; }

        public RegisterModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            User = new Users();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostAsync()
        {
            ModelState["User.Roles"].ValidationState = ModelValidationState.Valid;
            if (!ModelState.IsValid)
                return Page();

            try
            {
                if(_unitOfWork.UsersRepository.GetFirstOrDefault(u => u.Email == User.Email) == null)
                {
                    // Set Cookie
                    var cookieOptions = new CookieOptions()
                    {
                        Expires = DateTime.Now.AddMinutes(20),
                        IsEssential = true,
                        HttpOnly = true,
                        Secure = true
                    };
                    Response.Cookies.Append("UserToken", User.PublicId, cookieOptions);

                    // Set Role For User
                    _unitOfWork.UsersRepository.SetRole(User, "User");

                    // Add To DB
                    _unitOfWork.UsersRepository.Add(User);
                    _unitOfWork.Save();

                    // Success
                    ViewData["Notif"] = $"Welcome {User.Username}";
                    return RedirectToPage("./../Index");
                }
                else
                {
                    // Faild
                    ViewData["Notif"] = "Email has been registered!";
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
