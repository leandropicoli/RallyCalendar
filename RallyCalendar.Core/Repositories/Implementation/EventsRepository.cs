using RallyCalendar.Core.Models;
using System.Text.Json;

namespace RallyCalendar.Core.Repositories
{
    public class EventsRepository : BaseHttpRepository, IEventsRepository
    {
        public async Task<IEnumerable<Event>> GetEvents(string championship, int year)
        {
            var endpoint = $"content/filters/calendar?{championship}=wrc&origin=vcms&year={year}";
            var response = await Client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var externalEvents = JsonSerializer.Deserialize<Models.ExternalModels.Events>(responseBody);

            var events = new List<Event>();

            if (externalEvents == null || externalEvents.Content == null || externalEvents.Content.Length == 0)
            {
                return events;
            }

            foreach (var content in externalEvents.Content)
            {
                var rally = new Event
                {
                    Championship = championship,
                    Country = content.Location,
                    EndDate = content.EndDateLocal,
                    Id = content.EventId,
                    Name = content.Title,
                    Round = content.Round,
                    StartDate = content.StartDateLocal,
                    Year = content.ReleaseYear
                };
            }

            return events;
        }
    }
}
