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
            NextPageCommand = new Command(NextPage);
            SearchCommand = new Command<string>(SearchMovies);
        }

        public async override Task InitializeAsync(object navigationData)
        {
            IsLoading = true;

            MainPageModel = navigationData != null ? (MainPageModel)navigationData : new MainPageModel();
            
            PageNumber = MainPageModel.PageNumber == 0 ? 1 : MainPageModel.PageNumber;

            UpcomingMoviesModel result = null;

            if (MainPageModel.IsSearch)
            {
                result = await MoviesService.SearchMoviesAsync(MainPageModel.SearchString);
                UpcomingMoviesList = result.Results;
                ShowNextButton = false;
            }
            else
            {
                result = await MoviesService.GetUpcomingMoviesAsync();
                UpcomingMoviesList = result.Results;
                MainPageModel.TotalPages = result.Total_pages;
                ShowNextButton = MainPageModel.PageNumber < MainPageModel.TotalPages ? true : false;
            }

            IsLoading = false;
        }

        #region Properties

        public Command NavigateToDetailsCommand { get; set; }
        public Command NextPageCommand { get; set; }
        public Command SearchCommand { get; set; }

        private MainPageModel _mainPageModel;
        public MainPageModel MainPageModel
        {
            get => _mainPageModel;
            set => SetProperty(ref _mainPageModel, value);
        }

        private int _pageNumber;
        public int PageNumber
        {
            get => _pageNumber;
            set => SetProperty(ref _pageNumber, value);
        }

        private bool _showNextButton;
        public bool ShowNextButton
        {
            get => _showNextButton;
            set => SetProperty(ref _showNextButton, value);
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

        public async void NextPage()
        {
            MainPageModel.IsSearch = false;
            MainPageModel.SearchString = string.Empty;
            MainPageModel.PageNumber = PageNumber + 1;
            await navigation.NavigateToAsync<MainPageViewModel>(MainPageModel);
        }

        public async void SearchMovies(string param)
        {
            MainPageModel.IsSearch = true;
            MainPageModel.SearchString = param;
            await navigation.NavigateToAsync<MainPageViewModel>(MainPageModel);
        }

        #endregion

    }
}
