using Android.Widget;
using BusMap.Mobile.Droid;
using BusMap.Mobile.Helpers;

[assembly:Xamarin.Forms.Dependency(typeof(ToastMessageDroid))]
namespace BusMap.Mobile.Droid
{
    public class ToastMessageDroid : IToastMessage
    {
        public void LongTime(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortTime(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}