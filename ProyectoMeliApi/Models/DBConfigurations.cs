using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Models
{
    public class DBConfigurations
    {
        public string dbName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string hostname { get; set; }
        public string port { get; set; }
    }
}
