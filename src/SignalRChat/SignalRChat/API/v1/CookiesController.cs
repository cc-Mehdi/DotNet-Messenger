using Microsoft.AspNetCore.Mvc;
using DataLayer.Repositories.IRepositories;

namespace SignalRChat.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CookiesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CookiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/v1/Cookies/GetCookie
        // Route: /api/v1/Cookies/GetAllCookies
        [HttpGet("GetCookie")]
        public ActionResult<string> GetCookie(string cookieName)
        {
            try
            {
                // Try to get the cookie from the Request object
                if (Request.Cookies.TryGetValue(cookieName, out string cookieValue))
                    // Return the cookie value if it exists
                    return Ok(cookieValue);

                // Return NotFound if the cookie is not present
                return NotFound("Cookie not found.");
            }
            catch (Exception ex)
            {
                // Return NotFound if the cookie is not present
                return NotFound("Something was wrong\n" + ex.Message);
            }
        }

        // POST: api/v1/Cookies/SetCookie
        // Route: /api/v1/Cookies/SetCookie?cookieName=[name]&cookieValue=[value]&expireDays=[optional]
        [HttpPost("SetCookie")]
        public IActionResult SetCookie(string cookieName, string cookieValue, int? expireDays = null)
        {
            var cookieOptions = new CookieOptions();

            // Set an optional expiration date
            if (expireDays.HasValue)
                cookieOptions.Expires = DateTime.Now.AddDays(expireDays.Value);
            else
                cookieOptions.Expires = DateTime.Now.AddMinutes(20); // Default: 20 minutes

            // Set the cookie in the response
            Response.Cookies.Append(cookieName, cookieValue, cookieOptions);

            return Ok($"Cookie '{cookieName}' set successfully.");
        }

        // PUT: api/v1/Cookies/UpdateCookie
        // Route: /api/v1/Cookies/UpdateCookie?cookieName=[name]&cookieValue=[newValue]&expireDays=[optional]
        [HttpPut("UpdateCookie")]
        public IActionResult UpdateCookie(string cookieName, string cookieValue, int? expireDays = null)
        {
            if (Request.Cookies.ContainsKey(cookieName))
            {
                var cookieOptions = new CookieOptions();

                // Set the expiration for the updated cookie
                if (expireDays.HasValue)
                    cookieOptions.Expires = DateTime.Now.AddDays(expireDays.Value);
                else
                    cookieOptions.Expires = DateTime.Now.AddMinutes(20); // Default: 20 minutes

                // Update the cookie
                Response.Cookies.Append(cookieName, cookieValue, cookieOptions);
                return Ok($"Cookie '{cookieName}' updated successfully.");
            }

            return NotFound("Cookie not found to update.");
        }

        // DELETE: api/v1/Cookies/DeleteCookie
        // Route: /api/v1/Cookies/DeleteCookie?cookieName=[name]
        [HttpDelete("DeleteCookie")]
        public IActionResult DeleteCookie(string cookieName)
        {
            if (Request.Cookies.ContainsKey(cookieName))
            {
                // Set the cookie's expiration to a past date to remove it
                Response.Cookies.Delete(cookieName);
                return Ok($"Cookie '{cookieName}' deleted successfully.");
            }

            return NotFound("Cookie not found.");
        }

    }
}
