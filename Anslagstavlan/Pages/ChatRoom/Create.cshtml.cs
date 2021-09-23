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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Anslagstavlan.Pages.ChatRoom
{
    [Authorize]
    public class CreateRoomModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ChatUserModel> _userManager;
        private readonly AuthDbContext _context;

        [BindProperty]
        public ChatRoomModel ChatRoom { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public CreateRoomModel
            (AuthDbContext context, UserManager<ChatUserModel> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var chatRoom = new ChatRoomModel { ChatRoomOwner = user.ChatUserId, ChatRoomName = ChatRoom.ChatRoomName };

            if (Photo != null)
            {

                string folder = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string file = Path.Combine(folder, ChatRoom.PhotoPath);

                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }

                string uniqueFileName = String.Concat(Guid.NewGuid().ToString(), "-", ChatRoom.ChatRoomName.ToLower(), ".jpg");

                string uploadsFolder = Path.Combine(folder, uniqueFileName);

                using (var fileStream = new FileStream(uploadsFolder, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

                _context.ChatRoomModels.Where(x => x.ChatRoomId == chatRoom.ChatRoomId).FirstOrDefault().PhotoPath = uniqueFileName;
            }


            _context.Add(chatRoom);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ChatRoom/Index");
        }
    }
}
