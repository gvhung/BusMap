using System;
using System.Collections.Generic;
using System.Text;
using BusMap.Mobile.SQLite.Models;

namespace BusMap.Mobile.SQLite.Repositories
{
    public interface IRecentSearchRepository
    {
        event EventHandler<string> RecentSearchEvent;

        IEnumerable<RecentSearch> GetRecentSearches();
        void AddSearch(string start, string destination);
    }
}
