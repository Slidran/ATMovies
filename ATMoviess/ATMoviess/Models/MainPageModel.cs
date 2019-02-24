using ATMoviess.Services;

namespace ATMoviess.Models
{
    public class MainPageModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public MoviesService UpcomingMoviesService { get; set; }
    }
}
