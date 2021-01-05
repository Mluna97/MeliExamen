using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using Newtonsoft.Json;

using ProyectoMeliApi;
using ProyectoMeliApi.Controllers;

namespace ProyectoMeliApi.Tests
{
    public class MutantTests
    {
        [Fact]
        public Task IsMutant_ReturnForbidResult_GivenEmptyArray()
        {
            var controller = new MutanteController();
            ActionResult response = (ActionResult)controller.IsMutant(new string[] { });

            var obj = Assert.IsType<ObjectResult>(response) as ObjectResult;
            Assert.Equal(403, obj.StatusCode);

            return null;
        }

        [Fact]
        public Task IsMutant_ReturnOkResult_GivenMutantADN_MultipleCauses()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" });
            Assert.IsType<OkObjectResult>(response);
            return null;
        }

        [Fact]
        public Task IsMutant_ReturnOkResult_GivenMutantADN_RightDiagonal()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGCGA", "CAGTGC", "TTATCT", "AGAAGG", "CACCTA", "TCACTG" });
            Assert.IsType<OkObjectResult>(response);
            return null;
        }

        [Fact]
        public Task IsMutant_ReturnOkResult_GivenMutantADN_Vertical()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGCGA", "CGGTGC", "TTATGT", "AGAAGG", "CACCTA", "TCACTG" });
            Assert.IsType<OkObjectResult>(response);
            return null;
        }

        [Fact]
        public Task IsMutant_ReturnOkResult_GivenMutantADN_Horizontal()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGCGA", "CGGTGC", "TTATCT", "AGAAGG", "CCCCTA", "TCACTG" });
            Assert.IsType<OkObjectResult>(response);
            return null;
        }

        [Fact]
        public Task IsMutant_ReturnOkResult_GivenMutantADN_LeftDiagonal()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGCGA", "CAGTGC", "TTATCT", "AGACGG", "CCCATA", "TCACTG" });
            Assert.IsType<OkObjectResult>(response);
            return null;
        }

        [Fact]
        public Task IsMutant_ReturnForbidResult_GivenNonMutantADN_V1()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGATG", "GGATAG", "CTGGAT", "CACGTG", "GACTAC", "GCATAT" });

            var obj = Assert.IsType<ObjectResult>(response) as ObjectResult;
            Assert.Equal(403, obj.StatusCode);

            return null;
        }

        [Fact]
        public Task IsMutant_ReturnForbidResult_GivenNonMutantADN_DNAFormatNotNxN_V1()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGATG", "GGATAG", "CTGGAT", "CACGTG", "GACTAC" });

            var obj = Assert.IsType<ObjectResult>(response) as ObjectResult;
            Assert.Equal(403, obj.StatusCode);
            Assert.Equal("El DNA no es de NxN", obj.Value);

            return null;
        }

        [Fact]
        public Task IsMutant_ReturnForbidResult_GivenNonMutantADN_DNAFormatNotNxN_V2()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGAG", "GGATG", "CTGGA", "CACGT", "GACTA", "GCAAT" });

            var obj = Assert.IsType<ObjectResult>(response) as ObjectResult;
            Assert.Equal(403, obj.StatusCode);
            Assert.Equal("El DNA no es de NxN", obj.Value);

            return null;
        }
    }
}
