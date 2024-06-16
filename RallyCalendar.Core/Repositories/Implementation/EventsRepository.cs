using Microsoft.Extensions.Caching.Memory;
using RallyCalendar.Core.Models;
using RallyCalendar.Core.Repositories.Implementation;

namespace RallyCalendar.Core.Repositories
{
    public class EventsRepository : CacheHttpRepository, IEventsRepository
    {
        public EventsRepository(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<IEnumerable<Event>> GetEvents(string championship, int year)
        {
            var endpoint = $"content/filters/calendar?championship={championship}&origin=vcms&year={year}";
            var cacheKey = $"Events-{championship}-{year}";
            var response = await GetAsync<Models.ExternalModels.Events>(endpoint, cacheKey);

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
