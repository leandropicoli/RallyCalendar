using RallyCalendar.Core.Models;

namespace RallyCalendar.Core.Repositories
{
    public class EventsRepository : BaseHttpRepository, IEventsRepository
    {
        public async Task<IEnumerable<Event>> GetEvents(string championship, int year)
        {
            var endpoint = $"content/filters/calendar?{championship}=wrc&origin=vcms&year={year}";
            var response = await Client.GetAsync(endpoint);

            var events = new List<Event>();


            return events;
        }
    }
}
