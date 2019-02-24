using Xamarin.Forms;

namespace ATMoviess.Views
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page root) : base(root)
        {
            Init();
        }

        public CustomNavigationPage()
        {
            Init();
        }

        void Init()
        {
            BarBackgroundColor = Color.FromHex("#3FACFF");
            BarTextColor = Color.White;
        }
    }
}