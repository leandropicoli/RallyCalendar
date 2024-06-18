using RallyCalendar.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallyCalendar.Core.Fetchers.Abstraction
{
    public interface IEventsFetcher
    {
        Task<IEnumerable<Event>> GetEvents(string championship, int year);
    }
}
