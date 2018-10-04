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
            var databaseString = @"Data Source=LAPTOP-QRV40OSS;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            if (String.IsNullOrEmpty(databaseString))
            {
                throw new Exception("Please insert database connection string!");
            }

            return databaseString;
        }
    }
}
