using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anslagstavlan.Domain.Database;
using Anslagstavlan.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Anslagstavlan.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly AuthDbContext _context;

        [BindProperty]
        public ChatUserModel ChatUser { get; set; }

        public IndexModel(AuthDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {

        }
    }
}
