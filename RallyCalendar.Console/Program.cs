// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using RallyCalendar.Core.Repositories;

// Build the configuration
IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

Console.WriteLine("Hello, World!");

Console.WriteLine("Fetching events...");
var championship = "wrc";
int year = DateTime.UtcNow.Year;

var eventsRepo = new EventsRepository();

var events = await eventsRepo.GetEvents(championship, year);
