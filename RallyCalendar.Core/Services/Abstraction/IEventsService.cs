namespace RallyCalendar.Core.Services.Abstraction;
public interface IEventsService
{
    Task HandleEventsAsync(string championship, int? year);

    Task<IEnumerable<Models.Event>> GetEventsAsync(string championship, int? year);
}
