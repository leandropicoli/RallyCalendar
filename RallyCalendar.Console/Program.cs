using RallyCalendar.Core.Repositories;

Console.WriteLine("Hello, World!");

Console.WriteLine("Fetching events...");
var championship = "wrc";
int year = DateTime.UtcNow.Year;

var eventsRepo = new EventsRepository();

var events = await eventsRepo.GetEvents(championship, year);
foreach (var item in events)
{
    Console.WriteLine(item.ToString());
}

Console.ReadLine();