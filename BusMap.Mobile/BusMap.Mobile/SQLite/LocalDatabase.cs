using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BusMap.Mobile.Helpers;
using SQLite;
using Xamarin.Forms;

namespace BusMap.Mobile.SQLite
{
    public class LocalDatabase : ILocalDatabase
    {
        private readonly SQLiteConnection _connection;
        private readonly IFileHelper _fileHelper = DependencyService.Get<IFileHelper>();
        private readonly string _dbName;


        public LocalDatabase()
        {
            _dbName = "localDb.db3";
            var connectionString = _fileHelper.GetLocalFilePath(_dbName);
            CheckIfDbExist();
            _connection = new SQLiteConnection(connectionString);

            _connection.CreateTable<FavoriteRoute>();
        }

        public SQLiteConnection GetConnection()
            => _connection;


        private void CheckIfDbExist()
        {
            if (_fileHelper.CheckIfFileExist(_dbName))
            {
                _fileHelper.CreateDirectory(_dbName);
            }
        }
    }
}
