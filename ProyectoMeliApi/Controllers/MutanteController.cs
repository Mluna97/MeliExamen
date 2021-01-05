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
        MutantRepository repo;

        public MutanteController ()
        {
            repo = new MutantRepository();
        }
        [HttpPost]
        [Route("mutant")]
        public IActionResult IsMutant([FromBody] string[] dna)
        {
            DTOAdn dtoAdn = repo.GetAdn(dna);

            bool esMutante = dtoAdn.EsMutante ?? MutanteHelper.IsMutant(dna);

            if (!dtoAdn.EsMutante.HasValue)
                repo.InsertAdn(new DTOAdn(dna, esMutante));

            if (esMutante)
                return Ok();    // 200
            else
                return StatusCode(403);
        }

        [HttpGet]
        [Route("stats")]
        public JsonResult GetStats()
        {
            DTOStats stats = repo.GetStats();

            StatModel statsModel = new StatModel()
            {
                CountHumanDNA = stats.CountHumanDNA,
                CountMutantDNA = stats.CountMutantDNA
            };

            return Json(statsModel);
        }
    }
}
