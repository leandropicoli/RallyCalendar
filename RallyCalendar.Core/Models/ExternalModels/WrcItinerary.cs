using System.Text.Json.Serialization;

namespace RallyCalendar.Core.Models.ExternalModels;
public class WrcItinerary
{
    //TODO: add fields
    [JsonPropertyName("values")]
    public List<WrcItineraryInfo> Values { get; set; }
}

public class WrcItineraryInfo
{
    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("values")]
    public List<List<string>> Values { get; set; }
}