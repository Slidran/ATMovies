using ATMoviess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ATMoviess.Services
{
    public class MoviesService : CommunicationService
    {
        private const string TMDB_API_URL = "https://api.themoviedb.org/3";
        private const string TMDB_API_KEY = "1f54bd990f1cdfb230adb312546d765d";

        private List<Genre> GenresList { get; set; }

        public MoviesService() : base(TMDB_API_URL, TMDB_API_KEY)
        {
        }

        private async Task GetGenresAsync()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", "en-US");

            var content = await GetAsync("genre/movie/list", parameters);
            var response = await content.Content.ReadAsStringAsync();

            GenresList = JsonConvert.DeserializeObject<GenresListModel>(response).Genres;
        }

        public async Task<UpcomingMoviesModel> GetUpcomingMoviesAsync(int pageNumber)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", "en-US");
            parameters.Add("page", pageNumber.ToString());

            var content = await GetAsync("movie/upcoming", parameters);
            var response = await content.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<UpcomingMoviesModel>(response);

            if (GenresList == null)
                await GetGenresAsync();

            foreach (var item in result.Results)
            {
                foreach (var item2 in item.Genre_ids)
                {
                    var genre = GenresList.Where(x => x.Id == item2).FirstOrDefault();
                    item.Genres = item2 == item.Genre_ids.Last() ? item.Genres + genre.Name : item.Genres + genre.Name + ", ";
                }
            }

            return result;
        }

        public async Task<UpcomingMoviesModel> SearchMoviesAsync(string movieName)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", "en-US");
            parameters.Add("query", movieName);

            var content = await GetAsync("search/movie", parameters);
            var response = await content.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<UpcomingMoviesModel>(response);

            if (GenresList == null)
                await GetGenresAsync();

            foreach (var item in result.Results)
            {
                foreach (var item2 in item.Genre_ids)
                {
                    var genre = GenresList.Where(x => x.Id == item2).FirstOrDefault();
                    item.Genres = item2 == item.Genre_ids.Last() ? item.Genres + genre.Name : item.Genres + genre.Name + ", ";
                }
            }

            return result;
        }
    }
}
