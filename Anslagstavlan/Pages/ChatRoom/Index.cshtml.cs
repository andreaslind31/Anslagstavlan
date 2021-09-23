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
    public class IndexModel : PageModel
    {
        private readonly AuthDbContext _context;

        public IList<ChatRoomModel> ListRoomModel { get; set; } 

        public IndexModel(AuthDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            ListRoomModel = await _context.ChatRoomModels.ToListAsync();
        }
    }
}
