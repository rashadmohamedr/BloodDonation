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

            u.UserID = (int)dt.Rows[0]["UserID"];
            u.Name = (string)dt.Rows[0]["Name"];
            u.Email = (string)dt.Rows[0]["Email"];
            u.Password = (string)dt.Rows[0]["Password"];
            u.Phone = (string)dt.Rows[0]["Phone"];
            u.DateOfBirth = dt.Rows[0]["DateOfBirth"].ToString();



        }
    }
}
