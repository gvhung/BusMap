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
            containerRegistry.RegisterForNavigation<NavigationPage>(); 
            containerRegistry.RegisterForNavigation<MainPage>();   
            containerRegistry.RegisterForNavigation<NearestStopsMapPage>();
            containerRegistry.RegisterForNavigation<RoutesListPage>(); 
            containerRegistry.RegisterForNavigation<BusStopsMapPage>();
            containerRegistry.RegisterForNavigation<TrackNewRoutePage, TrackNewRouteViewModel>();
            containerRegistry.RegisterForNavigation<AddNewBusStopPage, AddNewBusStopViewModel>();
            containerRegistry.RegisterForNavigation<EditBusStopPage, EditBusStopPageViewModel>();
            containerRegistry.RegisterForNavigation<AddNewCarrierPage, AddNewCarrierViewModel>();
            containerRegistry.RegisterForNavigation<RoutesQueuePage, RoutesQueueViewModel>();
            containerRegistry.RegisterForNavigation<QueuedRouteDetailsPage, QueuedRouteDetailsViewModel>();
            containerRegistry.RegisterForNavigation<RouteDetailsPage, RouteDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<TraceTrackingPage, TraceTrackingPageViewModel>();
            containerRegistry.RegisterForNavigation<RouteReportPage, RouteReportViewModel>();

            containerRegistry.Register<IDataService, ApiDataService>();
        }

    }
}
