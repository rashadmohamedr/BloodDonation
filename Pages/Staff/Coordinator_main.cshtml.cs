using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace BloodDonation.Pages.Staff
{
    public class Coordinator_mainModel : PageModel
    {
        public List<Event> Events { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadEventsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            // Redirect to an edit page or handle edit logic here
            // Example: return RedirectToPage("/EditEvent", new { id });
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            await RemoveEventAsync(id);
            await LoadEventsAsync(); // Reload events after removal
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            // Redirect to an add page or handle add logic here
            // Example: return RedirectToPage("/AddEvent");
            return Page();
        }

        private async Task LoadEventsAsync()
        {
            Events = new List<Event>();

            string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Name, Date, Location, Description FROM Events";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Events.Add(new Event
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Date = reader.GetDateTime(2),
                                Location = reader.GetString(3),
                                Description = reader.GetString(4)
                            });
                        }
                    }
                }
            }
        }

        private async Task RemoveEventAsync(int id)
        {
            string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Events WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public class Event
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
        }
    }
}