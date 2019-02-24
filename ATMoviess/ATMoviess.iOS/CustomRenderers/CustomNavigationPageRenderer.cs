using System;
using ATMoviess.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ATMoviess.Views.CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace ATMoviess.iOS.CustomRenderers
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = UIFont.FromName("Nunito-ExtraLight", 18.0f),
                ForegroundColor = UIColor.White
            };
        }
    }
}
