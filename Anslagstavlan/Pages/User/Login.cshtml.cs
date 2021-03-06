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
    public class LoginModel : PageModel
    {
        public readonly SignInManager<ChatUserModel> signInManager;

        [BindProperty]
        public Login Model { get; set; } //viewModel
        
        public LoginModel(SignInManager<ChatUserModel> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            
            if (ModelState.IsValid)
            {
                
                var identityResult = await signInManager.PasswordSignInAsync(Model.Username, Model.Password, isPersistent: Model.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToPage("/ChatRoom/Index");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                ModelState.AddModelError("", "Username or password incorrect");
            }
            return Page();
        }
    }
}
