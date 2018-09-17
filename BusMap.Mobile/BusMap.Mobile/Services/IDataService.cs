using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.Services
{
    public interface IDataService
    {
        Task<ObservableCollection<Pin>> GetPins();
        Task PostPins(Models.Pin pin);
    }
}
