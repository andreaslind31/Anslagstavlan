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
    public class DetailsModel : PageModel
    {
        private readonly Anslagstavlan.Domain.Database.AuthDbContext _context;

        public DetailsModel(Anslagstavlan.Domain.Database.AuthDbContext context)
        {
            _context = context;
        }

        public ChatRoomModel ChatRoomModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChatRoomModel = await _context.ChatRoomModels.FirstOrDefaultAsync(m => m.ChatRoomId == id);

            if (ChatRoomModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
