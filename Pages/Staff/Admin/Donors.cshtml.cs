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

        public List<User> Users { get; set; } = new List<User>();



        public DonorsModel (ILogger<DonorsModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }


        public void OnGet()
        {

            

           dt= dB.ReadTable("User");

<<<<<<< HEAD
            for (int i = 0; i < dt.Rows.Count; i++) {

                u = new Models.DBClasses.User();

                u.Id = (int)dt.Rows[i]["id"];
                u.Name = (string)dt.Rows[i]["Name"];
                u.Email = (string)dt.Rows[i]["email"];
                u.Password = (string)dt.Rows[i]["Pass"];
                u.Phone = (string)dt.Rows[i]["PNO"];
                u.DateOfBirth = (string)dt.Rows[i]["DOB"];

                Users.Add(u);

            }

           
=======
            u.UserID = (int)dt.Rows[0]["UserID"];
            u.Name = (string)dt.Rows[0]["Name"];
            u.Email = (string)dt.Rows[0]["Email"];
            u.Password = (string)dt.Rows[0]["Password"];
            u.Phone = (string)dt.Rows[0]["Phone"];
            u.DateOfBirth = dt.Rows[0]["DateOfBirth"].ToString();
>>>>>>> 95e5051507dbbbcef6723c91068451b98cbc41fc



        }
    }
}
