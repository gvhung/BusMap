using System;
using Android.App;
using Android.Content;

namespace BusMap.Mobile.Droid.Services
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new [] { "StopTrackingIntent" })]
    public class NotificationBroadcastReceiver : BroadcastReceiver
    {
        public event EventHandler GeolocationServiceStopped;

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent == null)
                return;

            if (intent.Action == "STOP_TRACKING")
            {
                GeolocationServiceStopped?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}