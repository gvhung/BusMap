using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Models;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.Services
{
    public interface IDataService
    {
        Task<ObservableCollection<Pin>> GetPins();
        Task PostPins(BusStop busStop);

        Task<ObservableCollection<Pin>> GetPinsForRoute(int routeId);

    }
}
