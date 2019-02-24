using ATMoviess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ATMoviess.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var viewModel = BindingContext as MainPageViewModel;
            viewModel.NavigateToDetailsCommand.Execute(e.SelectedItem);
        }
    }
}
