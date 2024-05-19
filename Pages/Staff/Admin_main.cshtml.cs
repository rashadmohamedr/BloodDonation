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
        public void OnGet()
        {
            var chartData = @"
        {
            type: 'bar',
            responsive: true,
            data:
            {
                labels: ['Admin', 'Coordinator', 'Donors', 'Teams', 'Events', 'Experiences'],
                datasets: [{
                    label: '# of',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                        ],
                    borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                        ],
                    borderWidth: 1
                }]
            },
            options:
            {
                scales:
                {
                    yAxes: [{
                        ticks:
                        {
                            beginAtZero: true
                        }
                    }]
                }
            }
        }";

            Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
            ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });
        }
    }
}
