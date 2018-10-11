using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.iOS;
using Foundation;
using GlobalToast;
using UIKit;

[assembly:Xamarin.Forms.Dependency(typeof(ToastMessageiOS))]
namespace BusMap.Mobile.iOS
{
    public class ToastMessageiOS : IToastMessage
    {
        public void LongTime(string message)
        {
            Toast.ShowToast(message).SetDuration(ToastDuration.Long);
        }

        public void ShortTime(string message)
        {
            Toast.ShowToast(message).SetDuration(ToastDuration.Regular);
        }
    }
}