using BloodDonation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonation.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly ILogger<SignUpModel> _logger;
        private DB dB { get; set; }
        public SignUpModel(ILogger<SignUpModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }
        public void OnGet()
        {
            Dictionary<String,String> keyValuePairs = new Dictionary<String,String>();
            keyValuePairs.Add("Name", "Aya");
            keyValuePairs.Add("Email", "Aya@Gmail.com");
            keyValuePairs.Add("Password", "Aya123");
            keyValuePairs.Add("Phone", "012345678");
            keyValuePairs.Add("DateOfBirth", "2015-12-17");
            dB.AddUserSignUp(keyValuePairs);
        }
    }
}
