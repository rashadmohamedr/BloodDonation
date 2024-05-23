using BloodDonation.Models.Chart;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BloodDonation.Models;


namespace BloodDonation.Pages.Staff
{
    public class Admin_mainModel : PageModel
    {
        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }
        private readonly ILogger<Admin_mainModel> _logger;
        private DB dB { get; set; }
        public Admin_mainModel(ILogger<Admin_mainModel> logger, DB dB)
        {

            _logger = logger;
            this.dB = dB;
        }
        public void OnGet()
        {
            //if (HttpContext.Session.GetString("UserID").ToCharArray()[0] =='A') { RedirectToPage("Index"); }

            int adminCount = dB.GetColumnCount("Admin"); // Or however your method is structured
            int coordinatorCount = dB.GetColumnCount("Coordinator");
            int donorCount = dB.GetColumnCount("Donor");
            int staffCount = dB.GetColumnCount("Staff"); // Make sure this aligns with your labels
            int teamCount = dB.GetColumnCount("Team");

            // ... Your existing code ...

            // Now, create your chart data structure
            var chartData = new
            {
                type = "bar",
                responsive = true,
                data = new
                {
                    labels = new[] { "Admin", "Coordinator", "Donors", "Teams", "Events", "Experiences" },
                    datasets = new[]
                    {
            new
            {
                label = "# of",
                data = new[] { adminCount, coordinatorCount, donorCount, staffCount, teamCount }, // Use the calculated values
                backgroundColor = new[]
                {
                    "rgba(255, 99, 132, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 206, 86, 0.2)",
                    "rgba(75, 192, 192, 0.2)",
                    "rgba(153, 102, 255, 0.2)",
                    "rgba(255, 159, 64, 0.2)"
                },
                borderColor = new[]
                {
                    "rgba(255, 99, 132, 1)",
                    "rgba(54, 162, 235, 1)",
                    "rgba(255, 206, 86, 1)",
                    "rgba(75, 192, 192, 1)",
                    "rgba(153, 102, 255, 1)",
                    "rgba(255, 159, 64, 1)"
                },
                borderWidth = 1
            }
        }
                },
                options = new
                {
                    scales = new
                    {
                        yAxes = new[]
                        {
                new
                {
                    ticks = new
                    {
                        beginAtZero = true
                    }
                }
            }
                    }
                }
            };

            // Serialize to JSON
            ChartJson = JsonConvert.SerializeObject(chartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

        }
    }
}
