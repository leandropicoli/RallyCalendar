using RallyCalendar.Core.Configuration;
using RallyCalendar.Core.Fetchers.Abstraction;
using RallyCalendar.Core.Models;

namespace RallyCalendar.Core.Fetchers.Implementation;

public class WrcEventsFetcher : BaseHttpFetcher, IEventsFetcher
{
    public WrcEventsFetcher() : base(ConfigurationManager.GetSetting("WrcEndpoint"))
    {
    }

    public async Task<IEnumerable<Event>> GetEvents(string championship, int year)
    {
        var endpoint = $"content/filters/calendar?championship={championship}&origin=vcms&year={year}";
        
        var response = await GetAsync<Models.ExternalModels.WrcEvents>(endpoint);

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
