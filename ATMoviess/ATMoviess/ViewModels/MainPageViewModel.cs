using ATMoviess.Models;
using ATMoviess.Services;
using ATMoviess.Services.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ATMoviess.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService navigation;
        MoviesService moviesService;

        public MainPageViewModel(INavigationService _navigation)
        {
            navigation = _navigation;
            NavigateToDetailsCommand = new Command(NavigateToDetails);
            SearchMovieTitleCommand = new Command(SearchMovieTitle);
        }

        public async override Task InitializeAsync(object navigationData)
        {
            IsLoading = true;

            moviesService = new MoviesService();
            
            var result = await moviesService.GetUpcomingMoviesAsync();
            UpcomingMoviesList = result;
            UpcomingMoviesListFiltered = result;

            IsLoading = false;
        }

        #region Properties

        public Command NavigateToDetailsCommand { get; set; }
        public Command SearchMovieTitleCommand { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private List<Result> _upcomingMoviesList;
        public List<Result> UpcomingMoviesList
        {
            get => _upcomingMoviesList;
            set => SetProperty(ref _upcomingMoviesList, value);
        }

        private List<Result> _upcomingMoviesListFiltered;
        public List<Result> UpcomingMoviesListFiltered
        {
            get => _upcomingMoviesListFiltered;
            set => SetProperty(ref _upcomingMoviesListFiltered, value);
        }

        #endregion

        #region Methods

        public async void NavigateToDetails(object parameter)
        {
            await navigation.NavigateToAsync<MovieDetailsViewModel>(parameter);
        }

        public void SearchMovieTitle(object parameter)
        {
            var newText = parameter as string;
            UpcomingMoviesList = UpcomingMoviesListFiltered.Where(x => x.Title.ToLower().Contains(newText.ToLower())).ToList();
        }

        #endregion

    }
}
