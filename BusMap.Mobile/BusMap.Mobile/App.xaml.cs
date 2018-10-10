using BusMap.Mobile.Views;
using System;
using BusMap.Mobile.Services;
using BusMap.Mobile.ViewModels;
using CommonServiceLocator;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Unity;
using Unity.ServiceLocation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BusMap.Mobile
{
    public partial class App : PrismApplication
    {

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }


        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();  //v
            containerRegistry.RegisterForNavigation<MainPage>();    //v
            containerRegistry.RegisterForNavigation<NearestStopsMapPage>();  //v
            containerRegistry.RegisterForNavigation<RoutesListPage>();  //v
            containerRegistry.RegisterForNavigation<BusStopsMapPage>(); //v
            containerRegistry.RegisterForNavigation<TrackNewRoutePage>();

            containerRegistry.Register<IDataService, ApiDataService>();
        }

    }
}
