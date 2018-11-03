using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Data
{
    public static class Connections
    {
        public static string GeDbConnectionString()
        {
            var databaseString = @"";
            if (String.IsNullOrEmpty(databaseString))
            {
                throw new Exception("Please insert database connection string!");
            }

            return databaseString;
        }

        public static string GetAzureMapsKey()
        {
            var key = "";
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("Please insert azure maps public key!");
            }

            return key;
        }

    }
}
