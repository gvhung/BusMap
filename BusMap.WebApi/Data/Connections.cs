using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Data
{
    public static class Connections
    {
        //public static string GeDbConnectionString()
        //{
        //    var databaseString = @"Data Source=DESKTOP-6TBR12R\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=BusMap";
        //    if (String.IsNullOrEmpty(databaseString))
        //    {
        //        throw new Exception("Please insert database connection string!");
        //    }

        //    return databaseString;
        //}

        public static string GetAzureMapsKey()
        {
            var key = "63YSGTfv4RH_5UHMclF0Z8AV3g4P_kyad8U0-j7tpTc";
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("Please insert azure maps public key!");
            }

            return key;
        }

    }
}
