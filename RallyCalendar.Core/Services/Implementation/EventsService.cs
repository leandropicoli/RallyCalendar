using RallyCalendar.Core.Fetchers.Abstraction;
using RallyCalendar.Core.Services.Abstraction;

namespace RallyCalendar.Core.Services.Implementation;

    public class EventsService : IEventsService
    {
        private readonly IEventsFetcher _fetcher;

        public EventsService(IEventsFetcher fetcher)
        {
            _fetcher = fetcher;
        }

        public async Task HandleEventsAsync(string championship, int? year)
        {
            if (string.IsNullOrWhiteSpace(championship))
                throw new Exception("Championship cannot be null or empty");

            if (year == null)
                year = DateTime.UtcNow.Year;

            var events = await _fetcher.GetEvents(championship, year.Value);

            foreach (var item in events)
            {
                Console.WriteLine(item.ToString());
            }

            //store on DB
        }
    }
