using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Data
{
    public class Connections
    {
        public static string GeDbConnectionString()
        {
            var databaseString = @"Data Source=DESKTOP-6TBR12R\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=BusMap";
            if (String.IsNullOrEmpty(databaseString))
            {
                throw new Exception("Please insert database connection string!");
            }

            return databaseString;
        }
    }
}
