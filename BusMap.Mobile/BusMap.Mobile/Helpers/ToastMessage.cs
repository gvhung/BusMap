using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BusMap.Mobile.Helpers
{
    public static class ToastMessage
    {
        public static void LongTime(string message)
        {
            DependencyService.Get<IToastMessage>().LongTime(message);
        }

        public static void ShortTime(string message)
        {
            DependencyService.Get<IToastMessage>().ShortTime(message);
        }
    }
}
