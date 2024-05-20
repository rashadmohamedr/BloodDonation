using BloodDonation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private DB dB { get; set; }
        public IndexModel(ILogger<IndexModel> logger, DB dB) {

            _logger = logger;
            this.dB = dB;
        }
  
        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }   

        public void OnGet()
        {

            Console.WriteLine("email" + "pass");
        }
        public IActionResult OnPostLogin(string email,string pass)
        {
             (string,string) data=dB.SignIn(email, pass);
            HttpContext.Session.SetString("UserID",data.Item2);
            Console.WriteLine(data);
            return RedirectToPage(data.Item1);

        }
    }
}
