using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anslagstavlan.Domain.Database;
using Anslagstavlan.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Anslagstavlan.Pages.ChatRoom
{
    [Authorize]
    public class EditRoomModel : PageModel
    {
        private readonly AuthDbContext _context;

        [BindProperty]
        public ChatRoomModel ChatRoomModel { get; set; }

        public EditRoomModel(AuthDbContext context)
        {
            _context = context;
        }

        
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ChatRoomModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatRoomModelExists(ChatRoomModel.ChatRoomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/ChatRoom/Index");
        }

        private bool ChatRoomModelExists(int id)
        {
            return _context.ChatRoomModels.Any(e => e.ChatRoomId == id);
        }
    }
}
