using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Anslagstavlan.Domain.Models
{
    public class ChatUserModel
    {
        [Key]
        public int ChatUserId { get; set; }
        public string Username { get; set; }
    }
}
