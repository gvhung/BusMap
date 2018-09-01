using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BusMap.Mobile.Droid;
using BusMap.Mobile.Helpers;
using Xamarin.Forms;

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