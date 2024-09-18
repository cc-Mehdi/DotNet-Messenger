using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(10, 60)]
        public string PublicId { get; set; }

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

        [Display(Name = "Picture")]
        [Required]
        [Range(5, 300)]
        public string PictureAddress { get; set; }

        [AllowNull]
        public bool IsDeleted { get; set; }
    }
}
