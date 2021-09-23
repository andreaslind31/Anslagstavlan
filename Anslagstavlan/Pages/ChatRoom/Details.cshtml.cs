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

namespace Anslagstavlan.Pages.ChatRoom
{
    public class DetailsModel : PageModel
    {
        private static int RoomId { get; set; }

        private readonly AuthDbContext _context;

        public UserManager<ChatUserModel> UserManager { get; }

        public List<ChatMessageModel> TempMessages { get; set; } = new List<ChatMessageModel>();

        public ChatRoomModel ChatRoom { get; set; }

        [BindProperty]
        public ChatMessageModel ChatMessage { get; set; }

        public DetailsModel(AuthDbContext context, UserManager<ChatUserModel> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public async Task OnGet(int id)
        {

            RoomId = id;

            var user = await UserManager.GetUserAsync(HttpContext.User);

            ChatRoom = _context.ChatRoomModels.FirstOrDefault(m => m.ChatRoomId == id);

            TempMessages = _context.ChatMessageModels.Include(x => x.ChatUser).Where(x => x.ChatRoomId == id).ToList();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }

            var user = await UserManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToPage("/User/Login");
            }
            ChatMessage.ChatUser = user;
            ChatMessage.ChatUserId = user.ChatUserId;
            
            ChatRoom = _context.ChatRoomModels.FirstOrDefault(x => x.ChatRoomId == RoomId);
            ChatMessage.ChatRoom = ChatRoom;
            ChatMessage.ChatRoomId = ChatRoom.ChatRoomId;
            ChatMessage.Date = DateTime.Now;
           
            
            var result = _context.Add(ChatMessage);
            // result = "lots of data"
           
            var state = result.State;
            // state = added

            await _context.SaveChangesAsync();
            
            return RedirectToPage("/ChatRoom/Details", new
            {
                Id = RoomId
            });
        }
    }
}
