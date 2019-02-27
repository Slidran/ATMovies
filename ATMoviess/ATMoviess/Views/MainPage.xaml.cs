using ATMoviess.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace ATMoviess.Views
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel ViewModel { get => BindingContext as MainPageViewModel; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchMovieTitleCommand.Execute(null);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.NavigateToDetailsCommand.Execute(e.Item);
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            ViewModel.LoadMoreCommand.Execute(e.Item);
        }
    }
}
