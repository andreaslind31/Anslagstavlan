using Anslagstavlan.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anslagstavlan.Domain.Database
{
    public class AuthDbContext: IdentityDbContext
    {
        public DbSet<ChatMessageModel> ChatMessageModels { get; set; }
        public DbSet<ChatRoomModel> ChatRoomModels { get; set; }
        public DbSet<ChatUserModel> ChatUserModels { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        
    }
}
