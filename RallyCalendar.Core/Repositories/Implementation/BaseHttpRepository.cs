using System.Configuration;

namespace RallyCalendar.Core.Repositories
{
    public abstract class BaseHttpRepository
    {
        internal readonly HttpClient Client;
        protected BaseHttpRepository()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WrcEndpoint"]);
        }
    }
}
