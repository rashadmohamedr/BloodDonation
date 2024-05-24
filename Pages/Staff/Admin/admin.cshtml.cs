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
    public class adminModel : PageModel
    {
        private readonly ILogger<adminModel> _logger;
        private DB dB { get; set; }
        public DataTable dt { get; set; }
        public Models.DBClasses.Admin a { get; set; }

        public adminModel(ILogger<adminModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }


       
        public void OnGet()
        {
        }
    }
}
