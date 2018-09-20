using BusMap.Mobile.Views;
using System;
using BusMap.Mobile.Services;
using CommonServiceLocator;
using Unity;
using Unity.ServiceLocation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BusMap.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            UnityIoC();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void UnityIoC()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IDataService, StaticCodeDataService>();

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
        }

    }
}
