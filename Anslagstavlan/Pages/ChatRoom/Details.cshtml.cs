using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Anslagstavlan.Domain.Database;
using Anslagstavlan.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Anslagstavlan.ViewModels;

namespace Anslagstavlan.Pages.ChatRoom
{
    public class DetailsModel : PageModel
    {
        private int RoomId { get; set; }

        private readonly AuthDbContext _context;

        // not working with UserManager !!
        private readonly UserManager<IdentityUser> _userManager;

        public List<ChatMessageModel> TempMessages { get; set; } = new List<ChatMessageModel>();


        [BindProperty]
        public ChatRoomModel ChatRoom { get; set; }

        [BindProperty]
        public ChatMessageModel ChatMessage { get; set; }

        public DetailsModel(AuthDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomId = (int)id;

            ChatRoom = await _context.ChatRoomModels.FirstOrDefaultAsync(m => m.ChatRoomId == id);

            if (ChatRoom == null)
            {
                return NotFound();
            }

            TempMessages = _context.ChatMessageModels.Include(x => x.ChatUser).Where(x => x.ChatRoomId == RoomId).ToList();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var chatUser = new ChatUserModel()
            {
                Email = user.Email,
                UserName = user.Email,
            };

            ChatRoom = _context.ChatRoomModels.FirstOrDefault(x => x.ChatRoomId == RoomId); // <- doesnt work properly !
            ChatMessage.ChatRoom = ChatRoom;
            ChatMessage.ChatRoomId = ChatRoom.ChatRoomId;
            ChatMessage.Date = DateTime.Now;
            ChatMessage.ChatUser = chatUser;
            ChatMessage.ChatUserId = chatUser.ChatUserId;
            

            _context.Add(ChatMessage);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ChatRoom/Details", new
            {
                ChatRoom.ChatRoomId
            });
        }
    }
}
