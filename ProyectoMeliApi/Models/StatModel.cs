using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Models
{
    public class StatModel
    {
        public int CountMutantDNA { get; set; }

        public int CountHumanDNA { get; set; }

        public double Ratio { get { return CountHumanDNA != 0 ? (double) CountMutantDNA / CountHumanDNA : 0; } }
    }
}
