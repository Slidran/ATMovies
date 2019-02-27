using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms;
using Android.Support.V7.Graphics.Drawable;
using Android.Content;
using Android.Graphics;
using ATMoviess.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(ATMoviess.Views.CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace ATMoviess.Droid.CustomRenderers
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        public CustomNavigationPageRenderer(Context context) : base(context)
        {
        }

        private Android.Support.V7.Widget.Toolbar toolbar;

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                toolbar = (Android.Support.V7.Widget.Toolbar)child;
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                var textView = (Android.Widget.TextView)e.Child;
                textView.Typeface = Typeface.CreateFromAsset(Context.ApplicationContext.Assets, "Nunito-ExtraLight.ttf");
                textView.TextSize = 18;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}
