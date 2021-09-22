using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Anslagstavlan.Domain.Models
{
    public class ChatUserModel: IdentityUser
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ChatUserId { get; set; }
    }
}
