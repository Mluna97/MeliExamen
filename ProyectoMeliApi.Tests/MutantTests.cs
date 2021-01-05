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
            ActionResult response = (ActionResult) controller.IsMutant(new string[] { });

            var obj = Assert.IsType<StatusCodeResult>(response) as StatusCodeResult;
            Assert.Equal(403, obj.StatusCode);
    
            return null;
        }

        [Fact]
        public Task IsMutant_ReturnOkResult_GivenMutantADN_V1()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" });
            Assert.IsType<OkResult>(response);
            return null;
        }

        [Fact]
        public Task IsMutant_ReturnForbidResult_GivenNonMutantADN_V1()
        {
            var controller = new MutanteController();
            var response = controller.IsMutant(new string[] { "ATGATG", "GGATAG", "CTGGAT", "CACGTG", "GACTAC", "GCATAT" });
            
            var obj = Assert.IsType<StatusCodeResult>(response) as StatusCodeResult;
            Assert.Equal(403, obj.StatusCode);

            return null;
        }
    }
}
