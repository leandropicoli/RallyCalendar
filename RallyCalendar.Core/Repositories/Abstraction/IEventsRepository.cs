using RallyCalendar.Core.Models;

namespace RallyCalendar.Core.Repositories
{
    public interface IEventsRepository
    {
        Task<IEnumerable<Event>> GetEvents(string championship, int year);
    }
}
