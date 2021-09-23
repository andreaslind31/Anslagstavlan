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
using Microsoft.AspNetCore.Identity;

namespace Anslagstavlan.Pages.ChatRoom
{
    [Authorize]
    public class CreateRoomModel : PageModel
    {
        private readonly UserManager<ChatUserModel> _userManager;
        private readonly AuthDbContext _context;

        [BindProperty]
        public string ChatRoomName { get; set; }

        public CreateRoomModel(AuthDbContext context, UserManager<ChatUserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var chatRoom = new ChatRoomModel { ChatRoomOwner = user.ChatUserId, ChatRoomName = ChatRoomName };
            _context.Add(chatRoom);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ChatRoom/Index");
        }
    }
}
