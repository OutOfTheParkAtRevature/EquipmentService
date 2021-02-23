using System;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;

using Repository;
using Service;
using Models;

using EquipmentService.Tests.Utils;

namespace EquipmentService.Tests {
    public class IntegrationTests : IClassFixture<EquipmentFactory<Startup>> {
        private EquipmentFactory<Startup> Factory;
        public IntegrationTests(EquipmentFactory<Startup> factory) {
            Factory = factory; 
        }
        private static HttpRequestMessage GenerateMessage(HttpMethod method, string uri) {
            HttpRequestMessage result = new HttpRequestMessage(method, uri);
            result.Headers.TryAddWithoutValidation("Content-Type", "application/json");
            return result;
        }
        //broken test
        //[Theory]
        //[InlineData("api/Equipment/")]
        //[InlineData("api/Equipment/items")]
        //[InlineData("api/Equipment/f1e7904b-2e5f-460a-a057-fe6645d7d6c5")]
        //[InlineData("api/Equipment/items/1")]
        //    public async void TestClientEndpointsGET(string uri) {
        //        var client = Factory.CreateClient();
        //        var request = GenerateMessage(HttpMethod.Get, uri);
        //        var response = await client.SendAsync(request);
        //        Assert.True(response.IsSuccessStatusCode);
        //    }
    }
}
