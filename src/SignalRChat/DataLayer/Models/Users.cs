using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Username")]
        [Required]
        [Range(5, 300)]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required]
        [Range(5, 300)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        [Range(5, 300)]
        public string Password { get; set; }
    }
}
