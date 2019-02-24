using ATMoviess.Services;

namespace ATMoviess.Models
{
    public class MainPageModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public bool IsSearch { get; set; }
        public string SearchString { get; set; }
    }
}
