using RallyCalendar.Core.Configuration;
using RallyCalendar.Core.Fetchers.Abstraction;
using RallyCalendar.Core.Models;
using System.Text.Json;

namespace RallyCalendar.Core.Fetchers.Implementation
{
    public class WrcEventsFetcher : IEventsFetcher
    {
        private readonly HttpClient _httpClient;
        public WrcEventsFetcher()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationManager.GetSetting("WrcEndpoint"));
        }

        public async Task<IEnumerable<Event>> GetEvents(string championship, int year)
        {
            var endpoint = $"content/filters/calendar?championship={championship}&origin=vcms&year={year}";
            var httpResponse = await _httpClient.GetAsync(endpoint);
            httpResponse.EnsureSuccessStatusCode();

            var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<Models.ExternalModels.WrcEvents>(httpResponseBody);

            var events = new List<Event>();

            if (response == null || response.Content == null || response.Content.Length == 0)
            {
                return events;
            }

            foreach (var content in response.Content)
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

                events.Add(rally);
            }

            return events;
        }
    }
}
