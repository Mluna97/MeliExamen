using Microsoft.Data.SqlClient;
using ProyectoMeliApi.DTO;
using ProyectoMeliApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Repository
{
    // Mock para no tocar la bd, como no necesita más, no necesito insertar o leer de ninguna lista
    public class AdnMockRepository : IAdnRepository
    {
        public AdnMockRepository()
        { }

        public DTOAdn Get(string[] adn)
        {
            return Get(new DTOAdn(adn));
        }

        public DTOAdn Get(DTOAdn adn)
        {
            DTOAdn retorno = new DTOAdn(adn.Adn);
            
            return retorno;
        }

        public bool Insert(DTOAdn adn)
        {
            bool retorno = true;

            return retorno;
        }
    }
}
