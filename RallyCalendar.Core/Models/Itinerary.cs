namespace RallyCalendar.Core.Models;
public class Itinerary
{
    public Itinerary()
    {
        SpecialStages = new List<SpecialStageInfo>();
    }

    public DateTime Date { get; set; }
    public List<SpecialStageInfo> SpecialStages { get; set; }
}

public class SpecialStageInfo
{
    public string Stage { get; set; }
    public string Name { get; set; }
    public string Distance { get; set; }
    public DateTime FirstCarTime { get; set; }
}
