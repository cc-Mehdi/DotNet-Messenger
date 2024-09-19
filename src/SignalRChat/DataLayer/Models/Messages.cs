using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer.Models
{
    public class Messages
    {
        public Messages()
        {
            this.PublicId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(60)]
        public string PublicId { get; set; }

        [ForeignKey("Users")]
        public int SenderId { get; set; }
        public Users Sender { get; set; }

        [Display(Name = "MessageContent")]
        [Required(ErrorMessage = "Message can not be empty!")]
        [MinLength(1, ErrorMessage = "Message length is out of range!")]
        [MaxLength(500, ErrorMessage = "Message length is out of range!")]
        [DataType(DataType.MultilineText)]
        public string MessageContent { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [AllowNull]
        public bool IsDeleted { get; set; }
    }
}
