using VB.API.Models;

namespace VB.Tests.ObjectBuilders
{
    internal class RequestPropertiesBuilder
    {
        public RequestProperties Build()
        {
            return new RequestProperties
            {
                FilmName = "Name",
                TimeStamp = DateTime.Now,
                ImdbID = "ABC123",
                RequestResponseTime = 1000,
                UserIpAddress = "127.0.0.1"
            };
        }
    }
}
