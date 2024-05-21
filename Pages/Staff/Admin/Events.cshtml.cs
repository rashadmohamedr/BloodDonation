using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BloodDonation.Models;
using System.Data;



namespace BloodDonation.Pages.Staff.Admin
{
    public class EventsModel : PageModel
    {

        private readonly ILogger<EventsModel> _logger;
        private DB dB { get; set; }
        public DataTable dt { get; set; }
        public Models.DBClasses.AnEvent e { get; set; }

        public List<Models.DBClasses.AnEvent> AnEvent { get; set; } = new List<Models.DBClasses.AnEvent>();



        public EventsModel(ILogger<EventsModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }


        public void OnGet()
        {



            dt = dB.ReadTable("AnEvent");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                e = new Models.DBClasses.AnEvent();

                e.EventID = (int)dt.Rows[i]["EventID"];
                e.EventDate = (string)dt.Rows[i]["EventDate"];
                e.location = (string)dt.Rows[i]["Location"];
                e.Description = (string)dt.Rows[i]["EventDescription"];
                e.Name = (string)dt.Rows[i]["Name"];
               

                AnEvent.Add(e);

            }







        }
    }

    
    
}
