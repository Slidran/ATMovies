using ATMoviess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATMoviess.Services
{
    public interface IMoviesService
    {
        Task GetGenresAsync();
        Task<UpcomingMoviesModel> GetUpcomingMoviesAsync(int pageNumber);
        Task<UpcomingMoviesModel> SearchMoviesAsync(string movieName);
    }
}
