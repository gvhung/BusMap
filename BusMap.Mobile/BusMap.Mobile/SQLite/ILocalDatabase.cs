using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BusMap.Mobile.SQLite
{
    public interface ILocalDatabase
    {
        SQLiteConnection GetConnection();
    }
}
