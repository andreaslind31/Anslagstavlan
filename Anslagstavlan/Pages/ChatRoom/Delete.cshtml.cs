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
        private readonly Anslagstavlan.Domain.Database.AuthDbContext _context;

        public DeleteRoomModel(Anslagstavlan.Domain.Database.AuthDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChatRoomModel = await _context.ChatRoomModels.FindAsync(id);

            if (ChatRoomModel != null)
            {
                _context.ChatRoomModels.Remove(ChatRoomModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
