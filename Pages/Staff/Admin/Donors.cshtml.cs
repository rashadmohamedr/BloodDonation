using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BloodDonation.Models.;
using System.Data;
namespace BloodDonation.Pages.Staff.Admin
{
    public class DonorsModel : PageModel
    {
        private readonly ILogger<DonorsModel> _logger;
        private DB dB { get; set; }    

        public DataTable dt { get; set; }

        //public User u { get; set; }


        public DonorsModel (ILogger<DonorsModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }


        public void OnGet()
        {
           dt= dB.ReadTable("User");



        }
    }
}
