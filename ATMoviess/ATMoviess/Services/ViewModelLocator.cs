using ATMoviess.Services.Navigation;
using SimpleInjector;
using System;

namespace ATMoviess.Services
{
    public class ViewModelLocator
    {
        private static Container container;

        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        public ViewModelLocator()
        {
            container = new Container();
            container.Register<INavigationService, NavigationService>(Lifestyle.Singleton);
            container.Register<IMoviesService, MoviesService>(Lifestyle.Transient);
        }

        public object Resolve(Type type) => container.GetInstance(type);

        public T Resolve<T>() where T : class => container.GetInstance<T>();
    }
}
