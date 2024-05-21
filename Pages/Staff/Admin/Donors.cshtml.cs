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
        public Models.DBClasses.Donor d {  get; set; }

        public List<Models.DBClasses.Donor> Donor { get; set; } = new List<Models.DBClasses.Donor>();



        public DonorsModel (ILogger<DonorsModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }


        public void OnGet()
        {

            

           dt= dB.ReadTable("Donor");

            for (int i = 0; i < dt.Rows.Count; i++) {

                d = new Models.DBClasses.Donor();

                d.DonorID = (int)dt.Rows[i]["DonorID"];
                d.BloodType = (string)dt.Rows[i]["BloodType"];
                d.Gender = (string)dt.Rows[i]["Gender"];
                d.Travel = (string)dt.Rows[i]["Travel"];
                d.MedicationHistory = (string)dt.Rows[i]["MedicationHistory"];
                d.Weight = (int)dt.Rows[i]["Weight"];
                d.IllnessHistory = (string)dt.Rows[i]["IllnessHistory"];
                d.DonationInterval = (string)dt.Rows[i]["DonationInterval"];
                d.EligibilityStatus = (string)dt.Rows[i]["EligibilityStatus"];


                Donor.Add(d);

            }

           
            



        }
    }
}
