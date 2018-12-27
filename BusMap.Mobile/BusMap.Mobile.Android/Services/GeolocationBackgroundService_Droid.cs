using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using BusMap.Mobile.Droid.Services;
using BusMap.Mobile.Helpers;
using Plugin.CurrentActivity;
using Plugin.Geolocator;
using Xamarin.Forms;

[assembly:Dependency(typeof(GeolocationBackgroundService_Droid))]
namespace BusMap.Mobile.Droid.Services
{
    [Service]
    public class GeolocationBackgroundService_Droid : Service, IGeolocationBackgroundService
    {
        private static readonly string CHANNEL_ID = "geolocationServiceChannel";
        private Context _context = CrossCurrentActivity.Current.Activity;
        private NotificationManager _notificationManager;
        private NotificationBroadcastReceiver _broadcastReceiver;

        public override IBinder OnBind(Intent intent)
            => null;

        public GeolocationBackgroundService_Droid()
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CreateNotificationChannel();
            _broadcastReceiver = new NotificationBroadcastReceiver();
            _broadcastReceiver.GeolocationServiceStopped += async (s, e) =>
                await StopServiceAsync();

            RegisterReceiver(_broadcastReceiver, new IntentFilter("STOP_TRACKING"));
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            StopForeground(true);
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //notification pending intent
            var newIntent = new Intent(this, typeof(MainActivity));
            newIntent.AddFlags(ActivityFlags.ClearTop);
            newIntent.AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, newIntent, 0);

            //actions pending intent
            var stopTrackingIntent = new Intent("StopTrackingIntent");
            stopTrackingIntent.SetAction("STOP_TRACKING");
            stopTrackingIntent.SetFlags(ActivityFlags.NewTask);
            stopTrackingIntent.SetFlags(ActivityFlags.ClearTask);
            var pendingIntentStopTracking = PendingIntent.GetBroadcast(_context, 0, 
                stopTrackingIntent, PendingIntentFlags.CancelCurrent);

            var notification = new NotificationCompat.Builder(this, CHANNEL_ID)
                .SetContentIntent(pendingIntent)
                .SetSmallIcon(Resource.Drawable.ic_media_play_light)
                .SetAutoCancel(false)
                .SetShowWhen(true)
                .SetContentTitle("BusMap trace tracking")
                .SetContentText("Service is started.")
                .AddAction(Resource.Drawable.ic_media_stop_light, "Stop tracking", pendingIntentStopTracking)
                .Build();

            StartForeground(112, notification);
            return StartCommandResult.Sticky;
        }


        public async Task StartService()
        {
            var serviceIntent = new Intent(_context, typeof(GeolocationBackgroundService_Droid));
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                _context.StartForegroundService(serviceIntent);
            }
            else
            {
                _context.StartService(serviceIntent);
            }

            await StartTracking();
        }

        public async Task StopServiceAsync()
        {
            _context.StopService(new Intent(_context, typeof(GeolocationBackgroundService_Droid)));
            await CrossGeolocator.Current.StopListeningAsync();
        }




        private void CreateNotificationChannel()
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)                 
            {
                NotificationChannel serviceChannel = new NotificationChannel(CHANNEL_ID,
                    "GeolocationService", NotificationManager.ImportanceDefault);
                _notificationManager = _context.GetSystemService(Context.NotificationService) as NotificationManager;

                _notificationManager.CreateNotificationChannel(serviceChannel);
            }
            else
            {
                _notificationManager =
                    _context.GetSystemService(Context.NotificationService) as NotificationManager;
            }
        }

        private async Task StartTracking()
            => await CrossGeolocator.Current.StartListeningAsync(
                TimeSpan.FromSeconds(5), 5, true, new Plugin.Geolocator.Abstractions.ListenerSettings
                {
                    ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                    AllowBackgroundUpdates = true,
                    PauseLocationUpdatesAutomatically = false
                });

    }
}