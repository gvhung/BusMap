using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BusMap.Mobile.EventArgsConverters
{
    internal class PunctualityPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result;
            var valueString = value as string;
            valueString = valueString.Substring(0, 2);

            int.TryParse(valueString, out result);
            if (result > 49)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

