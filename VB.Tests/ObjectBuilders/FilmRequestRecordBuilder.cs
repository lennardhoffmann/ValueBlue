using VB.Data.Models;

namespace VB.Tests.ObjectBuilders
{
    internal class FilmRequestRecordBuilder
    {

        public FilmRequest Build()
        {
            return new FilmRequest
            {
                Search_Token = "Police Academy",
                imdbID = "tt0087928",
                IP_Address = "::1",
                TimeStamp = DateTime.Now,
                Processing_Time_Ms = 1000,
            };
        }
    }
}
