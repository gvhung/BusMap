using BusMap.Mobile.CustomControls;
using BusMap.Mobile.Views;
using BusMap.Mobile.Services;
using BusMap.Mobile.SQLite;
using BusMap.Mobile.SQLite.Repositories;
using BusMap.Mobile.ViewModels;
using Plugin.Iconize;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BusMap.Mobile
{
    public partial class App : PrismApplication
    {

        public App() : this(null)
        {

        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {

        }


        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MainMasterDetailPage/CustomNavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainMasterDetailPage, MainMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomNavigationPage>(nameof(CustomNavigationPage));           
            //containerRegistry.RegisterForNavigation<IconTabbedPage>(nameof(IconTabbedPage));

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();   
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
            containerRegistry.RegisterForNavigation<FavoritesPage, FavoritesPageViewModel>();
            containerRegistry.RegisterForNavigation<WeekDaySelectionPage, WeekDaySelectionPageViewModel>();
            containerRegistry.RegisterForNavigation<AdvancedSearchPage, AdvancedSearchPageViewModel>();

            containerRegistry.Register<IDataService, ApiDataService>();           
            containerRegistry.Register<ILocalDatabase, LocalDatabase>();
            containerRegistry.Register<IFavoriteRoutesRepository, FavoriteRoutesRepository>();
            containerRegistry.RegisterSingleton<IRecentSearchRepository, RecentSearchRepository>();
        }

    }
}
