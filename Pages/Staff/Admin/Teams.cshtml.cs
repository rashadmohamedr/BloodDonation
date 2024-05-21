using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BloodDonation.Models;
using System.Data;


namespace BloodDonation.Pages.Staff.Admin
{
    public class TeamsModel : PageModel
    {

            private readonly ILogger<TeamsModel> _logger;
            private DB dB { get; set; }
            public DataTable dt { get; set; }
            public Models.DBClasses.Team t { get; set; }

            public List<Models.DBClasses.Team> Team { get; set; } = new List<Models.DBClasses.Team>();



            public TeamsModel(ILogger<TeamsModel> logger, DB dB)
            {

                _logger = logger;
                this.dB = dB;
            }


        public void OnGet()
        {



            dt = dB.ReadTable("Team");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                t = new Models.DBClasses.Team();

                t.TeamID = (int)dt.Rows[i]["TeamID"];
                t.Name = (string)dt.Rows[i]["Name"];


                Team.Add(t);

            }

        }
    }
}
