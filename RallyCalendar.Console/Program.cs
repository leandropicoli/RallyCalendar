using Microsoft.Extensions.DependencyInjection;
using RallyCalendar.Core.Fetchers.Implementation;
using RallyCalendar.Core.Services.Implementation;

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();

void ConfigureServices(ServiceCollection services)
{

}

Console.WriteLine("Hello, World!");

Console.WriteLine("Fetching events...");
var championship = "wrc";
int year = DateTime.UtcNow.Year;

//var wrcFetcher = new WrcEventsFetcher();

//var eventsService = new EventsService(wrcFetcher);
//await eventsService.HandleEventsAsync(championship, year);

var eventId = "452";
var itineraryFetcher = new WrcItineraryFetcher();
var itineraries = itineraryFetcher.GetItineraries(eventId);

Console.ReadLine();