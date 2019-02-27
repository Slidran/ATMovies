using ATMoviess.Models;
using ATMoviess.Services;
using ATMoviess.Services.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ATMoviess.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService navigation;
        IMoviesService moviesService;

        public MainPageViewModel(INavigationService _navigation, IMoviesService _moviesService)
        {
            navigation = _navigation;
            moviesService = _moviesService;
            NavigateToDetailsCommand = new Command(NavigateToDetails);
            SearchMovieTitleCommand = new Command(SearchMovieTitle);
            LoadMoreCommand = new Command(LoadMore);
        }

        public async override Task InitializeAsync(object navigationData)
        {
            IsLoading = true;

            PageNumber = 1;

            //TODO: Put a empty state if no results or no internet access
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var upcomingMovies = await moviesService.GetUpcomingMoviesAsync(PageNumber);

                PageNumber++;
                TotalPages = upcomingMovies.TotalPages;
                
                UpcomingMoviesList = new ObservableCollection<Movie>(upcomingMovies.Results);
                UpcomingMoviesListFiltered = new ObservableCollection<Movie>(upcomingMovies.Results);
            }

            IsLoading = false;
        }

        #region Properties

        public Command NavigateToDetailsCommand { get; set; }
        public Command SearchMovieTitleCommand { get; set; }
        public Command LoadMoreCommand { get; set; }

        private ObservableCollection<Movie> _upcomingMoviesList;
        public ObservableCollection<Movie> UpcomingMoviesList
        {
            get => _upcomingMoviesList;
            set => SetProperty(ref _upcomingMoviesList, value);
        }

        private ObservableCollection<Movie> _upcomingMoviesListFiltered;
        public ObservableCollection<Movie> UpcomingMoviesListFiltered
        {
            get => _upcomingMoviesListFiltered;
            set => SetProperty(ref _upcomingMoviesListFiltered, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private int _pageNumber;
        public int PageNumber
        {
            get => _pageNumber;
            set => SetProperty(ref _pageNumber, value);
        }

        private int _totalPages;
        public int TotalPages
        {
            get => _totalPages;
            set => SetProperty(ref _totalPages, value);
        }

        #endregion

        #region Methods

        public async void NavigateToDetails(object parameter)
        {
            await navigation.NavigateToAsync<MovieDetailsViewModel>(parameter);
        }

        public void SearchMovieTitle()
        {
            SearchText = SearchText ?? string.Empty;
            UpcomingMoviesList = new ObservableCollection<Movie>(UpcomingMoviesListFiltered.Where(x => x.Title.ToLower().Contains(SearchText.ToLower())));
        }

        public async void LoadMore(object parameter)
        {
            //TODO: Put a IsBusy indicator so it does not load two times if something happens
            var movieItem = parameter as Movie;
            if (movieItem == UpcomingMoviesList.Last() && PageNumber <= TotalPages)
            {
                var upcomingMovies = await moviesService.GetUpcomingMoviesAsync(PageNumber);

                PageNumber++;

                foreach (var item in upcomingMovies.Results)
                {
                    UpcomingMoviesList.Add(item);
                }
            }
        }

        #endregion

    }
}
