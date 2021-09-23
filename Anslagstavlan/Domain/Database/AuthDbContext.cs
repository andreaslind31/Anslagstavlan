using Anslagstavlan.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Anslagstavlan.Domain.Database
{
    public class AuthDbContext: IdentityDbContext<ChatUserModel>
    {
        public DbSet<ChatMessageModel> ChatMessageModels { get; set; }
        public DbSet<ChatRoomModel> ChatRoomModels { get; set; }
        public DbSet<ChatUserModel> ChatUserModels { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        
    }
}
