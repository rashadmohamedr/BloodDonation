using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BloodDonation.Models;
using BloodDonation.Models.DBClasses;
using System.Data;
namespace BloodDonation.Pages.Staff.Admin
{
    public class DonorsModel : PageModel
    {
        private readonly ILogger<DonorsModel> _logger;
        private DB dB { get; set; }    

        public DataTable dt { get; set; }
        public Models.DBClasses.User u {  get; set; }


        public DonorsModel (ILogger<DonorsModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }


        public void OnGet()
        {

            u = new Models.DBClasses.User();

           dt= dB.ReadTable("User");

            u.Id = (int)dt.Rows[0]["id"];
            u.Name = (string)dt.Rows[0]["Name"];
            u.Email = (string)dt.Rows[0]["email"];
            u.Password = (string)dt.Rows[0]["Pass"];
            u.PhoneNumber = (string)dt.Rows[0]["PNO"];
            u.DateOfBirth = (string)dt.Rows[0]["DOB"];



        }
    }
}
