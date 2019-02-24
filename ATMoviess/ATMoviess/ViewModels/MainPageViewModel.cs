using ATMoviess.Models;
using ATMoviess.Services;
using ATMoviess.Services.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ATMoviess.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService navigation;

        public MainPageViewModel(INavigationService _navigation)
        {
            navigation = _navigation;

            NavigateToDetailsCommand = new Command(NavigateToDetails);
            SearchCommand = new Command<string>(SearchMovies);
        }

        public async override Task InitializeAsync(object navigationData)
        {
            IsLoading = true;

            Model = navigationData != null ? (MainPageModel)navigationData : new MainPageModel();

            UpcomingMoviesModel result = null;

            if (!string.IsNullOrEmpty(Model.SearchString))
            {
                Model.Title = "Search";
                Model.IsSearchVisible = false;
                result = await MoviesService.SearchMoviesAsync(Model.SearchString);
                UpcomingMoviesList = result.Results;
            }
            else
            {
                Model.Title = "Upcoming movies";
                Model.IsSearchVisible = true;
                result = await MoviesService.GetUpcomingMoviesAsync();
                UpcomingMoviesList = result.Results;
            }

            IsLoading = false;
        }

        #region Properties

        public Command NavigateToDetailsCommand { get; set; }
        public Command SearchCommand { get; set; }

        private MainPageModel _model;
        public MainPageModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

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

        #endregion

        #region Methods

        public async void NavigateToDetails(object parameter)
        {
            await navigation.NavigateToAsync<MovieDetailsViewModel>(parameter);
        }

        public async void SearchMovies(string param)
        {
            Model.SearchString = param;
            await navigation.NavigateToAsync<MainPageViewModel>(Model);
        }

        #endregion

    }
}
