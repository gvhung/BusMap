using System;
using System.Collections.Generic;
using System.Text;
using BusMap.Mobile.SQLite.Models;

namespace BusMap.Mobile.SQLite.Repositories
{
    public interface IVotedQueuedRoutesRepository
    {
        void AddVotedRoute(VotedQueuedRoute votedRoute);
        IEnumerable<VotedQueuedRoute> GetAllVotedQueuedRoutes();
    }
}
