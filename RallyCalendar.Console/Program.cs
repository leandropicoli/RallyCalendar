using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.Extensions.DependencyInjection;
using RallyCalendar.Core.Fetchers.Implementation;
using RallyCalendar.Core.Services.Implementation;


var championships = new Dictionary<int, string>
{
    {1, "wrc" },
    {2, "erc" }
};

var selectedChampionship = string.Empty;

ShowSplashScreen();
HandleOptionsMenu();

int year = DateTime.UtcNow.Year;

var wrcFetcher = new WrcEventsFetcher();
var eventsService = new EventsService(wrcFetcher);
Console.WriteLine("");
Console.WriteLine($"Fetching {selectedChampionship} events. Please wait");
Console.WriteLine("");
var events = await eventsService.GetEventsAsync(selectedChampionship, year);

var eventOption = HandleEvents(events);

var itineraryFetcher = new WrcItineraryFetcher();
var itineraries = await itineraryFetcher.GetItineraries(eventOption.Id);

GenerateCallendarFile(itineraries, eventOption);

Console.ReadLine();

void ShowSplashScreen()
{
    string splash = @"
        ______      _ _                          
        | ___ \    | | |                         
        | |_/ /__ _| | |_   _                    
        |    // _` | | | | | |                   
        | |\ \ (_| | | | |_| |                   
        \_| \_\__,_|_|_|\__, |                   
                         __/ |                   
                        |___/                    
                   _                _            
                  | |              | |           
          ___ __ _| | ___ _ __   __| | __ _ _ __ 
         / __/ _` | |/ _ \ '_ \ / _` |/ _` | '__|
        | (_| (_| | |  __/ | | | (_| | (_| | |   
         \___\__,_|_|\___|_| |_|\__,_|\__,_|_|   
        ";

    Console.WriteLine(splash);
}

void HandleOptionsMenu()
{
    Console.WriteLine("Choose the Championship:");
    Console.WriteLine("1 - WRC");
    Console.WriteLine("2 - ERC");
    Console.WriteLine();

    var optionChosen = Console.ReadLine();
    if (int.TryParse(optionChosen, out var option))
    {
        if (!championships.TryGetValue(option, out selectedChampionship))
        {
            Console.WriteLine("Invalid option");
            HandleOptionsMenu();
        }

        Console.WriteLine($"You have choosen {selectedChampionship}");
        Console.WriteLine();
    }
}

void GenerateCallendarFile(IEnumerable<RallyCalendar.Core.Models.Itinerary> itineraries, RallyCalendar.Core.Models.Event @event)
{
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
                DtStart = new CalDateTime(stage.FirstCarTime.ToUniversalTime()),
                DtEnd = new CalDateTime(stage.FirstCarTime.ToUniversalTime().AddHours(1)), // assuming each stage takes an hour
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
    File.WriteAllText($"{@event.Name}.ics", serializedCalendar);

    Console.WriteLine("ICS file created successfully!");
}

RallyCalendar.Core.Models.Event HandleEvents(IEnumerable<RallyCalendar.Core.Models.Event> events)
{
    ShowRallysMenu(events);

    if (int.TryParse(Console.ReadLine(), out var eventOption))
    {
        var eventChosen = events.FirstOrDefault(x => x.Round == eventOption);
        if (eventChosen != null)
        {
            Console.WriteLine($"You have choosen: {eventChosen.Name}");
            return eventChosen;
        }
    }

    Console.WriteLine("Invalid Option");
    ShowRallysMenu(events);

    return null;
}

void ShowRallysMenu(IEnumerable<RallyCalendar.Core.Models.Event> events)
{
    foreach (var rallyEvent in events)
    {
        Console.WriteLine(rallyEvent.ToString());
    };

    Console.WriteLine("Choose an event by Round:");
}