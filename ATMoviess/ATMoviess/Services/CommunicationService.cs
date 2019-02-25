using ATMoviess.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ATMoviess.Services
{
    public static class CommunicationService
    {
        private const string API_URL = "https://api.themoviedb.org/3";
        private const string API_KEY = "1f54bd990f1cdfb230adb312546d765d";

        public static async Task<HttpResponseMessage> GetAsync(string endpoint, Dictionary<string, object> parameters)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpResponseMessage result = null;

            using (var client = new HttpClient(handler))
            {
                var URL = string.Format("{0}/{1}?api_key={2}&", API_URL, endpoint, API_KEY);

                foreach (var item in parameters)
                {
                    var param = string.Format("{0}={1}&", item.Key, item.Value);
                    URL = URL + param;
                }

                client.Timeout = TimeSpan.FromMinutes(3);
                result = await client.GetAsync(URL);
            }

            return result;
        }
    }
}
