using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Helpers
{
    public class DBHelper
    {
        public static string GetMeliConnectionString()
        {
            //var appConfig = ConfigurationManager.AppSettings;

            string dbname = "MeliApiDb";//appConfig["RDS_DB_NAME"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = "admin";//appConfig["RDS_USERNAME"];
            string password = "mypassword";//appConfig["RDS_PASSWORD"];
            string hostname = "melidb.cptvcpynwt2g.sa-east-1.rds.amazonaws.com";//appConfig["RDS_HOSTNAME"];
            string port = "1433";//appConfig["RDS_PORT"];

            return "Server=" + hostname + ";Database=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}