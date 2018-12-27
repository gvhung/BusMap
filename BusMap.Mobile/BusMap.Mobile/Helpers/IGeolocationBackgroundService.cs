using System.Threading.Tasks;

namespace BusMap.Mobile.Helpers
{
    public interface IGeolocationBackgroundService
    {
        void StartService();
        Task StopServiceAsync();
        Task StartTrackingAsync();       
        Task PauseTrackingAsync();
        Task ResumeTrackingAsync();
    }
}
