using BloodDonation.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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
        [BindProperty]
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Date)]
        public string BirthdayDate { get; set; }

        [BindProperty]
        [Required]
        public string Gender { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [BindProperty]
        public string UserType { get; set; }

        [BindProperty]
        public string Role { get; set; }

        [BindProperty]
        public string BloodType { get; set; }

        [BindProperty]
        public string Travel { get; set; }

        [BindProperty]
        public string MedicationHistory { get; set; }

        [BindProperty]
        [Required]
        [Range(1, 500, ErrorMessage = "Weight must be between 1 and 500.")]
        public string weight { get; set; }

        [BindProperty]
        public string Donation_interval_days { get; set; }

        [BindProperty]
        public string Donation_interval_months { get; set; }

        [BindProperty]
        public string Donation_interval_years { get; set; }
        public void OnGet()
        {
            Dictionary<String, String> keyValuePairs = new Dictionary<String, String>();
        }

        public IActionResult OnPostSignUp(string Name,
                                 string BirthdayDate,
                                 string Gender,
                                 string email,
                                 string Password,
                                 string Phone,
                                 string UserType,
                                 string Role,
                                 string BloodType,
                                 string Travel,
                                 string MedicationHistory,
                                 string weight,
                                 string Donation_interval_days,
                                 string Donation_interval_months,
                                 string Donation_interval_years)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>()
            {
                { "Name", Name },
                { "DateOfBirth", BirthdayDate },
                { "Gender", Gender },
                { "Email", email },
                { "Password", Password },
                { "Phone", Phone },
                { "UserType", UserType },
                { "Role", Role },
                { "BloodType", BloodType },
                { "Travel", Travel },
                { "MedicationHistory", MedicationHistory },
                { "Weight", weight },
                { "Donation_interval_days", Donation_interval_days },
                { "Donation_interval_months", Donation_interval_months },
                { "Donation_interval_years", Donation_interval_years }
            };

            foreach (KeyValuePair<string, string> property in properties)
            {
                Console.WriteLine($"{property.Key}: {property.Value}");
            }
            (string,string) data=dB.AddUserSignUp(properties);
            HttpContext.Session.SetString("UserID", data.Item2);
            Console.WriteLine(data);
            return RedirectToPage(data.Item1);


        }
    }
}
