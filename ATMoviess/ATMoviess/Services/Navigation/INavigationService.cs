using System;
using System.Threading.Tasks;
using ATMoviess.ViewModels;

namespace ATMoviess.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateStringToAsync(string navString);
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToAsync(Type viewModelType);
        Task NavigateToAsync(Type viewModelType, object parameter);
    }
}
