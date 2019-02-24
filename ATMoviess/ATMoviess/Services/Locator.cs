using ATMoviess.Services.Navigation;
using SimpleInjector;
using System;

namespace ATMoviess.Services
{
    public class Locator
    {
        private static Container container;

        public static Locator Instance { get; } = new Locator();

        public Locator()
        {
            container = new Container();
            container.Register<INavigationService, NavigationService>(Lifestyle.Singleton);
        }

        public object Resolve(Type type) => container.GetInstance(type);

        public T Resolve<T>() where T : class => container.GetInstance<T>();
    }
}
