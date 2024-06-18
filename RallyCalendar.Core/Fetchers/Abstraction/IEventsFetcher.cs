using RallyCalendar.Core.Models;

namespace RallyCalendar.Core.Fetchers.Abstraction;

public interface IEventsFetcher
{
    Task<IEnumerable<Event>> GetEvents(string championship, int year);
}
