using ATMoviess.Services;

namespace ATMoviess.Models
{
    public class MainPageModel
    {
        public int PageNumber { get; set; }
        public string SearchString { get; set; }
        public string Title { get; set; }
        public bool IsSearchVisible { get; set; }
    }
}
