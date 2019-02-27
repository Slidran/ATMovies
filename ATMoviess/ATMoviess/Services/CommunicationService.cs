using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ATMoviess.Services
{
    public static class CommunicationService
    {
        private const string API_URL = "https://api.themoviedb.org/3";
        private const string API_KEY = "ede4188f86c09925c3f0420c9ec95c6b";

        /// <summary>
        /// Common method to get TMDB API information asynchronously
        /// </summary>
        /// <param name="endpoint">Endpoint you want to call</param>
        /// <param name="parameters">Params you need to send</param>
        /// <returns></returns>
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
