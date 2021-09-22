using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Anslagstavlan.Domain.Database;
using Anslagstavlan.Domain.Models;

namespace Anslagstavlan.Pages.ChatRoom
{
    public class ListRoomModel : PageModel
    {
        private readonly AuthDbContext _context;

        public IList<ChatRoomModel> ChatRoomModel { get; set; } 

        public ListRoomModel(AuthDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            ChatRoomModel = await _context.ChatRoomModels.ToListAsync();
        }
    }
}
