using Microsoft.Extensions.DependencyInjection;
using RallyCalendar.Core.Repositories;

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();

void ConfigureServices(ServiceCollection services)
{
    services.AddMemoryCache();
    services.AddScoped<IEventsRepository, EventsRepository>();
}

Console.WriteLine("Hello, World!");

Console.WriteLine("Fetching events...");
var championship = "wrc";
int year = DateTime.UtcNow.Year;

var eventsRepo = serviceProvider.GetRequiredService<IEventsRepository>();

var events = await eventsRepo.GetEvents(championship, year);
foreach (var item in events)
{
    Console.WriteLine(item.ToString());
}

Console.ReadLine();