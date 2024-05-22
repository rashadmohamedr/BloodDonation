using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BloodDonation.Models;
using BloodDonation.Models.DBClasses;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;
namespace BloodDonation.Pages.Staff.Admin
{
    public class DonorsModel : PageModel
    {

        public DonorsModel(ILogger<DonorsModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }


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


        public string donor { get; set; }
        public SqlConnection con { get; set; }
        private readonly ILogger<DonorsModel> _logger;
        private DB dB { get; set; }    
        public DataTable dt { get; set; }
        public Models.DBClasses.Donor d {  get; set; }

        public List<Models.DBClasses.Donor> Donor { get; set; } = new List<Models.DBClasses.Donor>();





        public void OnGet()
        {

            donor = "BloodType";

            dt = dB.ReadTable("Donor");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

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
            Dictionary<String, String> keyValuePairs = new Dictionary<String, String>();
        }



        public void OnPostDonors(string DonorID, string Name,
                                 string BirthdayDate,
                                 string Gender,
                                 string email,
                                 string Password,
                                 string Phone,
                                 string UserType,
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
            (string, string) data = dB.AddDonor(properties);
            return ;



            // public void Add(Dictionary<String, string> Dict)
            //    {

            //con.Open();
            //string Q = $"INSERT INTO [Donor] ([DonorID],BloodType,Gender,Travel,MedicationHistory,IllnessHistory,DonationInterval,EligabilityStatus,Weight,UserId,TeamId,TeamLeaderID) VALUES ('{Dict["DonorID"]}','{Dict["BloodType"]}','{Dict["Gender"]}','{Dict["Travel"]}','{Dict["MedicationHistory"]}','{Dict["IllnessHistory"]}','{Dict["DonationInterval"]}','{Dict["EligabilityStatus"]}','{Dict["Weight"]}','{Dict["UserId"]}','{Dict["TeamId"]}','{Dict["TeamLeaderID"]}');";

            //        SqlCommand cmd = new SqlCommand(Q, con);
            //        cmd = new SqlCommand(Q, con);



            //                cmd.ExecuteNonQuery();
            ////redirect to donor_main


            //con.Close();
            //        OnGet();





            //    }


        }
    }
}
