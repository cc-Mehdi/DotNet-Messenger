using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer.Models
{
    public class Roles
    {
        public Roles()
        {
            this.PublicId = Guid.NewGuid().ToString();
            this.Title = "User";
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string PublicId { get; set; }

        [Display(Name = "Title")]
        [Required]
        [MinLength(3, ErrorMessage = "{0} length is out of range!")]
        [MaxLength(100, ErrorMessage = "{0} length is out of range!")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
    }
}
