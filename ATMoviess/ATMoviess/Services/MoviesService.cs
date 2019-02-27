using ATMoviess.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATMoviess.Services
{
    public class MoviesService : IMoviesService
    {
        private List<Genre> GenresList { get; set; }
        
        /// <summary>
        /// Gets the list of genres
        /// </summary>
        /// <returns></returns>
        public async Task GetGenresAsync()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", "en-US");

            var content = await CommunicationService.GetAsync("genre/movie/list", parameters);
            var response = await content.Content.ReadAsStringAsync();

            GenresList = JsonConvert.DeserializeObject<GenresListModel>(response).Genres;
        }
        
        /// <summary>
        /// Gets the the whole list of upcoming movies from the TMDB database.
        /// Orders the list by release date. Does not filter region.
        /// </summary>
        /// <returns>Ordered list of upcoming movies</returns>
        public async Task<UpcomingMoviesModel> GetUpcomingMoviesAsync(int pageNumber)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", "en-US");
            parameters.Add("page", pageNumber);
            //parameters.Add("region", "US");

            var content = await CommunicationService.GetAsync("movie/upcoming", parameters);
            var response = await content.Content.ReadAsStringAsync();

            DefaultContractResolver contractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = contractResolver };

            var result = JsonConvert.DeserializeObject<UpcomingMoviesModel>(response, jsonSerializerSettings);

            if (GenresList == null)
                await GetGenresAsync();
            
            foreach (var item in result.Results)
            {
                foreach (var item2 in item.GenreIds)
                {
                    var genre = GenresList.Where(x => x.Id == item2).FirstOrDefault();
                    item.Genres = item2 == item.GenreIds.Last() ? item.Genres + genre.Name : item.Genres + genre.Name + ", ";
                }
                if (string.IsNullOrEmpty(item.Genres))
                {
                    item.Genres = "No genres found";
                }
            }

            result.Results = result.Results.Where(x => x.ReleaseDateConverted >= DateTime.Today).ToList();

            return result;
        }

        /// <summary>
        /// Searches for movies in the whole TMDB database. NOT BEING USED.
        /// </summary>
        /// <param name="movieName">Movie title, full or partial</param>
        /// <returns></returns>
        public async Task<UpcomingMoviesModel> SearchMoviesAsync(string movieName)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", "en-US");
            parameters.Add("query", movieName);

            var content = await CommunicationService.GetAsync("search/movie", parameters);
            var response = await content.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<UpcomingMoviesModel>(response);

            if (GenresList == null)
                await GetGenresAsync();

            foreach (var item in result.Results)
            {
                foreach (var item2 in item.GenreIds)
                {
                    var genre = GenresList.Where(x => x.Id == item2).FirstOrDefault();
                    item.Genres = item2 == item.GenreIds.Last() ? item.Genres + genre.Name : item.Genres + genre.Name + ", ";
                }
                if (string.IsNullOrEmpty(item.Genres))
                {
                    item.Genres = "No genres found";
                }
            }

            result.Results = result.Results.OrderBy(x => x.ReleaseDate).ToList();

            return result;
        }
    }
}
