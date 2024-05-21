using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodDonation.Pages.User
{
    public class Donor_mainModel : PageModel
    {
        public List<EventInfo> ListEvents { get; private set; } = new List<EventInfo>();
        public List<TeamInfo> ListTeam { get; private set; } = new List<TeamInfo>();
        public List<ExperienceInfo> Listexperiences { get; private set; } = new List<ExperienceInfo>();

        [BindProperty]
        public ExperienceInfo NewExperience { get; set; }

        private readonly string _connectionString = "Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public void OnGet()
        {
            LoadEvents();
            LoadTeams();
            LoadExperience();  // Ensure experiences are loaded
        }

        private void LoadEvents()
        {
            ExecuteReader("SELECT EventID, Name, EventDate, Location, EventDescription FROM AnEvent", reader =>
            {
                var eventInfo = new EventInfo
                {
                    EventID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    EventDate = reader.GetString(2),
                    Location = reader.GetString(3),
                    EventDescription = reader.GetString(4)
                };
                ListEvents.Add(eventInfo);
            });
            Console.WriteLine($"Loaded {ListEvents.Count} events.");
        }

        private void LoadTeams()
        {
            ExecuteReader("SELECT TeamID, Name FROM Team", reader =>
            {
                var teamInfo = new TeamInfo
                {
                    TeamID = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                ListTeam.Add(teamInfo);
            });
            Console.WriteLine($"Loaded {ListTeam.Count} teams.");
        }

        private void LoadExperience()
        {
            ExecuteReader("SELECT ExperienceID, Name, Rating, Description FROM Experience", reader =>
            {
                var experienceInfo = new ExperienceInfo
                {
                    ExperienceID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Description = reader.GetString(3)
                };
                Listexperiences.Add(experienceInfo);
            });
            Console.WriteLine($"Loaded {Listexperiences.Count} experiences.");
        }

        private void ExecuteReader(string query, Action<SqlDataReader> readAction)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            readAction(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostSaveExperienceAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "INSERT INTO Experience (Name, Rating, Description) OUTPUT INSERTED.ExperienceID VALUES (@Name, @Rating, @Description)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", NewExperience.Name);
                        command.Parameters.AddWithValue("@Rating", NewExperience.Rating);
                        command.Parameters.AddWithValue("@Description", NewExperience.Description);

                        // Get the inserted ExperienceID
                        NewExperience.ExperienceID = (int)await command.ExecuteScalarAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting experience: {ex.Message}");
                // Handle the error appropriately in production code
            }

            return new JsonResult(NewExperience); // Return the inserted experience as JSON
        }

        public class EventInfo
        {
            public int EventID { get; set; }
            public string Name { get; set; }
            public string EventDate { get; set; }
            public string Location { get; set; }
            public string EventDescription { get; set; }
        }

        public class TeamInfo
        {
            public int TeamID { get; set; }
            public string Name { get; set; }
        }

        public class ExperienceInfo
        {
            public int ExperienceID { get; set; }
            public string Name { get; set; }
            public int Rating { get; set; }
            public string Description { get; set; }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles(); // This line enables serving static files from wwwroot
        }
    }
}
