using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.DTO
{
    public class DTOAdn
    {
        public DTOAdn(string[] adn)
        {
            Adn = adn;
        }

        public DTOAdn(string[] adn, bool esMutante)
        {
            Adn = adn;
            EsMutante = esMutante;
        }

        public string[] Adn { get; set; }

        public bool? EsMutante { get; set; }

        public string AdnFormateado
        {
            get
            {
                return string.Join("-", Adn);
            }
        }
    }
}
