using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.Extensions.DependencyInjection;
using RallyCalendar.Core.Fetchers.Implementation;

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

var eventId = "453";
var itineraryFetcher = new WrcItineraryFetcher();
var itineraries = await itineraryFetcher.GetItineraries(eventId);

// Create a new calendar
var calendar = new Ical.Net.Calendar();

foreach (var eventDate in itineraries)
{
    foreach (var stage in eventDate.SpecialStages)
    {
        var e = new CalendarEvent
        {
            Summary = $"{stage.Stage} - {stage.Name}",
            Description = $"Stage: {stage.Stage}\nName: {stage.Name}\nDistance: {stage.Distance}",
            DtStart = new CalDateTime(stage.FirstCarTime),
            DtEnd = new CalDateTime(stage.FirstCarTime.AddHours(1)), // assuming each stage takes an hour
            Location = stage.Name,
            Uid = Guid.NewGuid().ToString(),
            Sequence = 0
        };
        calendar.Events.Add(e);
    }
}

// Serialize the calendar to a string
var serializer = new CalendarSerializer();
var serializedCalendar = serializer.SerializeToString(calendar);

// Write the serialized calendar to a file
File.WriteAllText("events.ics", serializedCalendar);

Console.WriteLine("ICS file created successfully!");

Console.ReadLine();