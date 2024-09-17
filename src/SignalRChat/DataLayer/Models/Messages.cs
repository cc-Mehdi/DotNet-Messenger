using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int SenderId { get; set; }
        public Users Sender { get; set; }

        [Display(Name = "MessageContent")]
        [Required(ErrorMessage = "Message can not be empty!")]
        [Range(1, 500, ErrorMessage = "Message length is out of range!")]
        public string MessageContent { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
