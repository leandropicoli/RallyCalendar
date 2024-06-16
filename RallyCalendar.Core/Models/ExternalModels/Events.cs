using System.Text.Json.Serialization;

namespace RallyCalendar.Core.Models.ExternalModels
{
    public class Events
    {
        [JsonPropertyName("content")]
        public Content[] Content { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("eventId")]
        public string EventId { get; set; }

        [JsonPropertyName("round")]
        public int Round { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("releaseYear")]
        public int ReleaseYear { get; set; }

        [JsonPropertyName("startDateLocal")]
        public DateTime StartDateLocal { get; set; }

        [JsonPropertyName("endDateLocal")]
        public DateTime EndDateLocal { get; set; }
    }
}
