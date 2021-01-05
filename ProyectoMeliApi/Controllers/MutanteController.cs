using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoMeliApi.DTO;
using ProyectoMeliApi.Helpers;
using ProyectoMeliApi.Models;
using ProyectoMeliApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MutanteController : Controller
    {
        IAdnRepository AdnRepo = new AdnRepository();
        IStatsRepository StatsRepo = new StatsRepository();

        public MutanteController(IAdnRepository adnRepository = null)
        {
            if (adnRepository != null)
                AdnRepo = adnRepository;
        }

        [HttpPost]
        [Route("mutant")]
        public IActionResult IsMutant([FromBody] string[] dna)
        {
            bool esValido = dna.Where(x => x.Length != dna.Length).ToList().Count == 0;
            bool esMutante = false;

            if (esValido)
            {
                DTOAdn dtoAdn = AdnRepo.Get(dna);

                esMutante = dtoAdn.EsMutante ?? MutanteHelper.IsMutant(dna);

                if (!dtoAdn.EsMutante.HasValue)
                    AdnRepo.Insert(new DTOAdn(dna, esMutante));
            }

            if (esMutante)
                return Ok("El DNA ingresado es de un MUTANTE");    // 200
            else
                return StatusCode(403, esValido ? "El DNA ingresado NO es de un mutante" : "El DNA no es de NxN");
        }

        [HttpGet]
        [Route("stats")]
        public JsonResult GetStats()
        {
            DTOStats stats = StatsRepo.Get();

            StatsModel statsModel = new StatsModel()
            {
                CountHumanDNA = stats.CountHumanDNA,
                CountMutantDNA = stats.CountMutantDNA
            };

            return Json(statsModel);
        }
    }
}
