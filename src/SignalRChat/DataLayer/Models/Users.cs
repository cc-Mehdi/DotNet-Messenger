using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer.Models
{
    public class Users
    {
        public Users()
        {
            this.PublicId = Guid.NewGuid().ToString();
            this.PictureAddress = "./Customers/UserImages/pic_profile_default.svg";
            this.IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string PublicId { get; set; }

        [Display(Name = "Username")]
        [Required]
        [MinLength(3, ErrorMessage = "{0} length is out of range!")]
        [MaxLength(100, ErrorMessage = "{0} length is out of range!")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        [MinLength(8, ErrorMessage = "Password must be minum 8 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Picture")]
        [Required]
        [MinLength(5)]
        [MaxLength(300)]
        public string PictureAddress { get; set; }

        [AllowNull]
        public bool IsDeleted { get; set; }
    }
}
