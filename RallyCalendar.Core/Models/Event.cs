namespace RallyCalendar.Core.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string Championship { get; set; }
        public string Id { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
