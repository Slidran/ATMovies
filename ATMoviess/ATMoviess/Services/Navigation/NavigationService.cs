using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATMoviess.Views;
using ATMoviess.ViewModels;
using Xamarin.Forms;
using System.Linq;

namespace ATMoviess.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> mappings;

        public NavigationService()
        {
            mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
        }

        private void CreatePageViewModelMappings()
        {
            mappings.Add(typeof(MainPageViewModel), typeof(MainPage));
            mappings.Add(typeof(MovieDetailsViewModel), typeof(MovieDetails));
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"${viewModelType} doesn't exists in _mappings");
            }
            return mappings[viewModelType];
        }

        private Page CreateAndBindPage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
                throw new Exception($"{viewModelType} isn't mapped to a page");

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;
            return page;
        }

        public async Task InitializeAsync()
        {
            Page page = CreateAndBindPage(typeof(MainPageViewModel));
            Application.Current.MainPage = new CustomNavigationPage(page);
            await (page.BindingContext as ViewModelBase).InitializeAsync(null);
        }

        public Task NavigateStringToAsync(string navString)
        {
            Type keyViewModel = null;
            string[] result = null;

            if (!string.IsNullOrEmpty(navString))
            {
                result = navString.Split('/');

                keyViewModel = mappings.Where(pair => pair.Value.Name == result[0])
                    .Select(pair => pair.Key)
                    .FirstOrDefault();

                if (keyViewModel != null)
                {
                    return NavigateToAsync(keyViewModel, result[1]);
                }
            }

            return null;
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }
        
        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType);
            
            var nav = Application.Current.MainPage as CustomNavigationPage;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await nav.PushAsync(page);
            });

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }
    }
}
