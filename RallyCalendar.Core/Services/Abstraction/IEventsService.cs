namespace RallyCalendar.Core.Services.Abstraction;
public interface IEventsService
{
    Task HandleEventsAsync(string championship, int? year);
}
