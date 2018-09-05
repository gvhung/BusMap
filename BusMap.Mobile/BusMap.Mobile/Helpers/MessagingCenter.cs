using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace BusMap.Mobile.Helpers
{
    public static class MessagingHelper
    {
        public static void DisplayAlert(string message)
        {
            var config = new AlertConfig
            {
                Title = "Alert!",
                Message = message
            };

            UserDialogs.Instance.Alert(config);
        }

        public static void DisplayAlert(string title, string message)
        {
            var config = new AlertConfig
            {
                Title = title,
                Message = message
            };

            UserDialogs.Instance.Alert(config);
        }

        public static void DisplayAlert(string title, string message, string okText)
        {
            var config = new AlertConfig
            {
                Title = title,
                Message = message,
                OkText = okText
            };

            UserDialogs.Instance.Alert(config);
        }

        public static void DisplayAlert(string title, string message, string okText, Action action)
        {
            var config = new AlertConfig
            {
                Title = title,
                Message = message,
                OkText = okText,
                OnAction = action
            };

            UserDialogs.Instance.Alert(config);
        }

        public static void Toast(string message, ToastTime toastTime)
        {
            switch (toastTime)
            {
                case ToastTime.ShortTime:
                    DependencyService.Get<IToastMessage>().ShortTime(message);
                    break;
                case ToastTime.LongTime:
                    DependencyService.Get<IToastMessage>().LongTime(message);
                    break;
            }
        }


    }

    public enum ToastTime : int
    {
        ShortTime,
        LongTime
    }
}
