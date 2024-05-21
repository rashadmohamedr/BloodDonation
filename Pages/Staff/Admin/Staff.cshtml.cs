using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BloodDonation.Models;
using System.Data;


namespace BloodDonation.Pages.Staff.Admin
{
    public class StaffModel : PageModel
    {
        private readonly ILogger<StaffModel> _logger;
        private DB dB { get; set; }
        public DataTable dt { get; set; }
        public Models.DBClasses.Staff s { get; set; }

        public List<Models.DBClasses.Staff> Staff { get; set; } = new List<Models.DBClasses.Staff>();



        public StaffModel(ILogger<StaffModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }


        public void OnGet()
        {



            dt = dB.ReadTable("Staff");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                s = new Models.DBClasses.Staff();

                s.StaffID = (int)dt.Rows[i]["StaffID"];
                s.YearsOfExperience = (int)dt.Rows[i]["YearsOfExperience"];
                s.Role = (string)dt.Rows[i]["Role"];


                Staff.Add(s);

            }







        }
    }
}
