using ATMoviess.Models;
using ATMoviess.Services.Navigation;
using System.Threading.Tasks;

namespace ATMoviess.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase
    {
        INavigationService navigation;

        public MovieDetailsViewModel(INavigationService _navigation)
        {
            navigation = _navigation;
        }

        public async override Task InitializeAsync(object navigationData)
        {
            MovieDetails = navigationData as Result;
        }

        #region Properties
        
        private Result _movieDetails;
        public Result MovieDetails
        {
            get => _movieDetails;
            set => SetProperty(ref _movieDetails, value);
        }

        #endregion

    }
}
