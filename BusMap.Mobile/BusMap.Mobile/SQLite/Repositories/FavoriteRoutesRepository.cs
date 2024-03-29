﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusMap.Mobile.SQLite.Models;
using SQLite;

namespace BusMap.Mobile.SQLite.Repositories
{
    public class FavoriteRoutesRepository : IFavoriteRoutesRepository
    {
        private readonly SQLiteConnection _connection;

        public FavoriteRoutesRepository(ILocalDatabase localDatabase)
        {
            _connection = localDatabase.GetConnection();
        }

        ~FavoriteRoutesRepository()
        {
            _connection.Close();
        }

        public void AddFavorite(FavoriteRoute favoriteRoute)
            => _connection.Insert(favoriteRoute);

        public FavoriteRoute GetFavorite(int id)
            => _connection.Table<FavoriteRoute>().First(f => f.Id == id);

        public IEnumerable<FavoriteRoute> GetAllFavorites()
            => _connection.Table<FavoriteRoute>().ToList();

        public void RemoveFavorite(int id)
            => _connection.Table<FavoriteRoute>().Delete(f => f.Id == id);

        public bool IsRouteInFavorites(int id)
            => _connection.Table<FavoriteRoute>().Any(f => f.Id == id);

            


    }
}
