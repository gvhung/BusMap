using System.Threading.Tasks;

namespace BusMap.Mobile.Helpers
{
    public interface IGeolocationBackgroundService
    {
        Task StartService();
        Task StopServiceAsync(); 
    }
}
