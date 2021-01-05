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


        //[Fact]
        //public async Task TestGet()
        //{
        //    var lambdaFunction = new LambdaEntryPoint();

        //    var requestStr = File.ReadAllText("./SampleRequests/ValuesController-Get.json");
        //    var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
        //    var context = new TestLambdaContext();
        //    var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        //    Assert.Equal(200, response.StatusCode);
        //    Assert.Equal("[\"value1\",\"value2\"]", response.Body);
        //    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
        //    Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
        //}

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
