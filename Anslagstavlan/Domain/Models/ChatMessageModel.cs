using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Anslagstavlan.Domain.Models
{
    public class ChatMessageModel
    {
        [Key]
        public int ChatMessageId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public int ChatRoomId { get; set; } //foreign key
        public ChatRoomModel ChatRoom { get; set; } // Don't forget .Include(x => x.ChatRoom)

        public int ChatUserId { get; set; } //foreign key
        public ChatUserModel ChatUser { get; set; } // Don't forget .Include(x => x.ChatUser)

    }
}
