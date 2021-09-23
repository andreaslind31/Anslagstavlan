using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Anslagstavlan.Domain.Database;
using Anslagstavlan.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Anslagstavlan.Pages.ChatRoom
{
    [Authorize]
    public class DeleteRoomModel : PageModel
    {
        private readonly AuthDbContext _context;

        [BindProperty]
        public ChatRoomModel ChatRoom { get; set; }

        public DeleteRoomModel(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChatRoom = await _context.ChatRoomModels.FirstOrDefaultAsync(m => m.ChatRoomId == id);

            if (ChatRoom == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChatRoom = await _context.ChatRoomModels.FindAsync(id);

            if (ChatRoom != null)
            {
                _context.ChatRoomModels.Remove(ChatRoom);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/ChatRoom/Index");
        }
    }
}
