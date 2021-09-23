using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anslagstavlan.Domain.Models
{
    public class ChatUserModel: IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChatUserId { get; set; }
    }
}
