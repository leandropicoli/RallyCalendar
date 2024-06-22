using RallyCalendar.Core.Configuration;
using RallyCalendar.Core.Fetchers.Abstraction;
using RallyCalendar.Core.Models;
using RallyCalendar.Core.Models.ExternalModels;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace RallyCalendar.Core.Fetchers.Implementation;
public class WrcItineraryFetcher : BaseHttpFetcher, IItineraryFetcher
{
    public WrcItineraryFetcher()
    {
        SetBaseAddress(ConfigurationManager.GetSetting("WrcEndpoint"));
    }

    public async Task<IEnumerable<Itinerary>> GetItineraries(string eventId)
    {
        var endpoint = $"content/result/itinerary?eventId={eventId}&extended=false";

        var response = await GetAsync<WrcItinerary>(endpoint);

        var result = new List<Itinerary>();

        if (response == null || response.Values == null || response.Values.Count == 0)
            return result;

        foreach (var dateEvent in response.Values)
        {
            // Remove the ordinal suffix (st, nd, rd, th) using Regex
            string cleanedDateString = Regex.Replace(dateEvent.Date, @"\b(\d+)(st|nd|rd|th)\b", "$1");

            // Define the format without the ordinal suffix
            string format = "dddd d MMMM";

            // Parse the cleaned date string
            DateTime currentDay = DateTime.ParseExact(cleanedDateString, format, CultureInfo.InvariantCulture);

            //set current yer if not possible to parse year from date
            if (currentDay.Year == 0 || currentDay.Year == DateTime.MinValue.Year)
            {
                currentDay = new DateTime(DateTime.UtcNow.Year, currentDay.Month, currentDay.Day);
            }

            var itinerary = new Itinerary
            {
                Date = currentDay
            };

            foreach(var stage in dateEvent.Values)
            {
                var specialStage = new SpecialStageInfo
                {
                    Stage = stage[0],
                    Distance = stage[3],
                    FirstCarTime = DateTime.Parse(stage[6]),
                    Name = stage[10]
                };
                itinerary.SpecialStages.Add(specialStage);
            }

            result.Add(itinerary);
        }

        return result;
    }
}
