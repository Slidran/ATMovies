using ATMoviess.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ATMoviess.Services
{
    public abstract class CommunicationService
    {
        private string API_URL { get; set; }
        private string API_KEY { get; set; }

        public CommunicationService(string apiURL, string apiKey = "")
        {
            API_URL = apiURL;
            API_KEY = apiKey;
        }

        protected async Task<HttpResponseMessage> GetAsync(string endpoint, Dictionary<string, object> parameters)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpResponseMessage result = null;

            using (var client = new HttpClient(handler))
            {
                var URL = string.Format("{0}/{1}?api_key=" + API_KEY + "&", API_URL, endpoint);

                foreach (var item in parameters)
                {
                    var paramss = string.Format("{0}={1}&", item.Key, item.Value);
                    URL = URL + paramss;
                }

                client.Timeout = TimeSpan.FromMinutes(3);
                result = await client.GetAsync(URL);
            }

            return result;
        }
    }
}
