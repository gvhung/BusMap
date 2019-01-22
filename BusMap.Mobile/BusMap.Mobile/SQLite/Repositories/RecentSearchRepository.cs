using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusMap.Mobile.SQLite.Models;
using SQLite;

namespace BusMap.Mobile.SQLite.Repositories
{
    public class RecentSearchRepository : IRecentSearchRepository
    {
        private readonly SQLiteConnection _connection;

        public RecentSearchRepository(ILocalDatabase localDatabase)
            => _connection = localDatabase.GetConnection();

        public event EventHandler<string> RecentSearchEvent;

        public IEnumerable<RecentSearch> GetRecentSearches()
            => _connection.Table<RecentSearch>().ToList();

        public void AddSearch(string start, string destination)
        {
            var test = GetRecentSearches();
            if (_connection.Table<RecentSearch>().Any(r => r.Start.Equals(start) && r.Destination.Equals(destination)))
            {
                return;
            }

            _connection.Insert(new RecentSearch
            {
                Start = start,
                Destination = destination
            });            

            if (_connection.Table<RecentSearch>().Count() > 5)
            {
                var minId = _connection.Table<RecentSearch>().Min(x => x.Id);
                _connection.Table<RecentSearch>().Delete(x => x.Id == minId);
            }

            var test2 = GetRecentSearches();
            RecentSearchEvent?.Invoke(this, $"{start},{destination}");
        }
    }
}
