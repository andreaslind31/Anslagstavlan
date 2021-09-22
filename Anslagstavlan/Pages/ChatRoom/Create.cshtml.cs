using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Anslagstavlan.Domain.Database;
using Anslagstavlan.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Anslagstavlan.Pages.ChatRoom
{
    [Authorize]
    public class CreateRoomModel : PageModel
    {
        private readonly AuthDbContext _context;

        [BindProperty]
        public ChatRoomModel ChatRoom { get; set; }

        public CreateRoomModel(AuthDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ChatRoomModels.Add(ChatRoom);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
