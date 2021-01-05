using Microsoft.EntityFrameworkCore;
using ProyectoMeliApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Repository
{
    public class MeliDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DBHelper.GetMeliConnectionString());
        }

        public static MeliDBContext Create()
        {
            return new MeliDBContext();
        }
    }
}
