using RallyCalendar.Core.Models;

namespace RallyCalendar.Core.Fetchers.Abstraction;
public interface IItineraryFetcher
{
    Task<IEnumerable<Itinerary>> GetItineraries(string eventId);
}
