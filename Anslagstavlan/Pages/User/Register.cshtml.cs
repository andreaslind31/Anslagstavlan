using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anslagstavlan.Domain.Models;
using Anslagstavlan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Anslagstavlan.Pages.User
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ChatUserModel> userManager;
        private readonly SignInManager<ChatUserModel> signInManager;

        public string Username { get; set; }
        public string Password { get; set; }

        public RegisterModel(UserManager<ChatUserModel> userManager, SignInManager<ChatUserModel> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ChatUserModel()
                {
                    UserName = Username,
                };

                var result = await userManager.CreateAsync(user, Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToPage("/ChatRoom/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
