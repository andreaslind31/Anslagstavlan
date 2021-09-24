using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Anslagstavlan.Domain.Database;
using Anslagstavlan.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Anslagstavlan.Pages.User
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AuthDbContext _context;

        private static string UserName;

        [BindProperty]
        public ChatUserModel ChatUser { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public EditModel(AuthDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> OnGetAsync(string name)
        {
            UserName = name;

            ChatUser = await _context.ChatUserModels.FirstOrDefaultAsync(m => m.UserName == name);

            if (ChatUser == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ChatUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatUserModelExists(ChatUser.ChatUserId))
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

                string file = Path.Combine(folder, ChatUser.PhotoPath);

                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }

                string uniqueFileName = String.Concat(Guid.NewGuid().ToString(), "-", ChatUser.UserName.ToLower(), ".jpg");

                string uploadsFolder = Path.Combine(folder, uniqueFileName);

                using (var fileStream = new FileStream(uploadsFolder, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

                _context.ChatUserModels.Where(x => x.UserName == UserName).FirstOrDefault().PhotoPath = uniqueFileName;

                int chatUserId = _context.ChatUserModels.Where(x => x.UserName == UserName).FirstOrDefault().ChatUserId;

                return RedirectToPage("/User/Edit", chatUserId );
            }
            return RedirectToPage("/User/Index");
        }

        private bool ChatUserModelExists(int chatUserId)
        {
            return _context.ChatUserModels.Any(e => e.ChatUserId == chatUserId);
        }
    }
}
