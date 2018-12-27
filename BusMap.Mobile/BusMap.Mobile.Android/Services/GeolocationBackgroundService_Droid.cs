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

        public override IBinder OnBind(Intent intent)
            => null;

        public GeolocationBackgroundService_Droid()
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CreateNotificationChannel();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //notification pending intent
            var newIntent = new Intent(this, typeof(MainActivity));
            newIntent.AddFlags(ActivityFlags.ClearTop);
            newIntent.AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, newIntent, 0);

            var notification = new NotificationCompat.Builder(this, CHANNEL_ID)
                .SetContentIntent(pendingIntent)
                .SetSmallIcon(Resource.Drawable.ic_media_play_light)
                .SetAutoCancel(false)
                .SetShowWhen(true)
                .SetContentTitle("BusMap trace tracking")
                .SetContentText("Service is started.")
                .Build();

            StartForeground(112, notification);
            return StartCommandResult.Sticky;
        }


        public void StartService()
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
        }

        public async Task StopServiceAsync()
        {
            throw new NotImplementedException();
        }

        public async Task StartTrackingAsync()
        {
            throw new NotImplementedException();
        }

        public async Task PauseTrackingAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ResumeTrackingAsync()
        {
            throw new NotImplementedException();
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

    }
}