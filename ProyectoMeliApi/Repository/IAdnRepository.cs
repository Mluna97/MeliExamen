using ProyectoMeliApi.DTO;
using ProyectoMeliApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Repository
{
    public interface IAdnRepository
    {
        DTOAdn Get(string[] adn);

        DTOAdn Get(DTOAdn adn);

        bool Insert(DTOAdn adn);

    }
}
