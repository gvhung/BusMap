using System;
using System.Collections.Generic;
using System.Text;
using BusMap.Mobile.SQLite.Models;

namespace BusMap.Mobile.SQLite.Repositories
{
    public interface IFavoriteRoutesRepository
    {
        void AddFavorite(FavoriteRoute favoriteRoute);
        FavoriteRoute GetFavorite(int id);
        IEnumerable<FavoriteRoute> GetAllFavorites();
        void RemoveFavorite(int id);
    }
}
