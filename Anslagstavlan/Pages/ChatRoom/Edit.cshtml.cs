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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Anslagstavlan.Pages.ChatRoom
{
    [Authorize]
    public class EditRoomModel : PageModel
    {
        private readonly AuthDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private static int RoomId;

        [BindProperty]
        public ChatRoomModel ChatRoom { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public EditRoomModel(AuthDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            RoomId = id;

            ChatRoom = await _context.ChatRoomModels.FirstOrDefaultAsync(m => m.ChatRoomId == id);

            if (ChatRoom == null)
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

            _context.Attach(ChatRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatRoomModelExists(ChatRoom.ChatRoomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

                _context.ChatRoomModels.Where(x => x.ChatRoomId == RoomId).FirstOrDefault().PhotoPath = uniqueFileName;

            }
            return RedirectToPage("/ChatRoom/Index");
        }

        private bool ChatRoomModelExists(int id)
        {
            return _context.ChatRoomModels.Any(e => e.ChatRoomId == id);
        }
    }
}
