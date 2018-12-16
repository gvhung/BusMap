using System;
using System.Collections.Generic;
using System.Text;
using BusMap.Mobile.SQLite.Models;
using SQLite;

namespace BusMap.Mobile.SQLite.Repositories
{
    public class VotedQueuedRoutesRepository : IVotedQueuedRoutesRepository
    {
        private readonly SQLiteConnection _connection;

        public VotedQueuedRoutesRepository(ILocalDatabase localDatabase)
        {
            _connection = localDatabase.GetConnection();
        }

        public void AddVotedRoute(VotedQueuedRoute votedRoute)
            => _connection.Insert(votedRoute);

        public IEnumerable<VotedQueuedRoute> GetAllVotedQueuedRoutes()
            => _connection.Table<VotedQueuedRoute>().ToList();
    }
}
