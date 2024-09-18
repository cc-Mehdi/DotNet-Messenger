using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(10, 60)]
        public string PublicId { get; set; }

        [ForeignKey("Users")]
        public int SenderId { get; set; }
        public Users Sender { get; set; }

        [Display(Name = "MessageContent")]
        [Required(ErrorMessage = "Message can not be empty!")]
        [Range(1, 500, ErrorMessage = "Message length is out of range!")]
        public string MessageContent { get; set; }

        public DateTime CreateDate { get; set; }

        [AllowNull]
        public bool IsDeleted { get; set; }
    }
}
