using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.Services
{
    public interface IDataService
    {
        ObservableCollection<Pin> GetPins();
    }
}
