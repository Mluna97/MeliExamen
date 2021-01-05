using ProyectoMeliApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Repository
{
    public interface IStatsRepository
    {
        DTOStats Get();
    }
}
