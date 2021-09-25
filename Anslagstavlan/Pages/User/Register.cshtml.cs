using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [BindProperty]
        public Register Model { get; set; } //viewModel


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
                    UserName = Model.Username,
                };

                var result = await userManager.CreateAsync(user, Model.Password);

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
