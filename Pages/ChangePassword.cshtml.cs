using BloodDonation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly ILogger<ChangePasswordModel> _logger;
        private DB dB { get; set; }
        public ChangePasswordModel(ILogger<ChangePasswordModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [BindProperty]
        [Required]
        public string newPassword { get; set; }
        [BindProperty]
        [Required]
        [Compare(nameof(newPassword), ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPostChangePassword()
        {
            if (ModelState.IsValid)
            {
            dB.changePassword(newPassword, email);
            return RedirectToPage("/Index");
            }
            else
            {
            return Page();

            }
        }
    }
}
