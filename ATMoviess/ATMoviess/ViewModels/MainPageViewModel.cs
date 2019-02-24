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
            BackToMainPageCommand = new Command(BackToMainPage);
        }

        public async override Task InitializeAsync(object navigationData)
        {
            MainPageModel = navigationData != null ? (MainPageModel)navigationData : new MainPageModel();
            
            PageNumber = MainPageModel.PageNumber == 0 ? 1 : MainPageModel.PageNumber;
            
            MainPageModel.UpcomingMoviesService = MainPageModel.UpcomingMoviesService == null ? new MoviesService() : MainPageModel.UpcomingMoviesService;

            var result = await MainPageModel.UpcomingMoviesService.GetUpcomingMoviesAsync(PageNumber);
            MainPageModel.TotalPages = result.Total_pages;
            UpcomingMoviesList = result.Results;

            ShowNextButton = MainPageModel.PageNumber < MainPageModel.TotalPages ? true : false;
            ShowBackButton = false;
        }

        #region Properties

        public Command NavigateToDetailsCommand { get; set; }
        public Command NextPageCommand { get; set; }
        public Command SearchCommand { get; set; }
        public Command BackToMainPageCommand { get; set; }

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

        private bool _showBackButton;
        public bool ShowBackButton
        {
            get => _showBackButton;
            set => SetProperty(ref _showBackButton, value);
        }

        private List<Result> _upcomingMoviesList;
        public List<Result> UpcomingMoviesList
        {
            get => _upcomingMoviesList;
            set => SetProperty(ref _upcomingMoviesList, value);
        }

        #endregion

        #region Methods

        public void NavigateToDetails(object parameter)
        {
            navigation.NavigateToAsync<MovieDetailsViewModel>(parameter);
        }

        public void NextPage()
        {
            MainPageModel.PageNumber = PageNumber + 1;
            navigation.NavigateToAsync<MainPageViewModel>(MainPageModel);
        }

        public async void SearchMovies(string param)
        {
            var result = await MainPageModel.UpcomingMoviesService.SearchMoviesAsync(param);
            UpcomingMoviesList = result.Results;
            ShowNextButton = false;
            ShowBackButton = true;
        }

        public async void BackToMainPage()
        {
            navigation.InitializeAsync();
            //PageNumber = 1;
            //var result = await MainPageModel.UpcomingMoviesService.GetUpcomingMoviesAsync(PageNumber);
            //MainPageModel.TotalPages = result.Total_pages;
            //UpcomingMoviesList = result.Results;
            //ShowNextButton = true;
            //ShowBackButton = false;
        }

        #endregion

    }
}
