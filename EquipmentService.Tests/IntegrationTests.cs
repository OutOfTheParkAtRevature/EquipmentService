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
        [Fact]
        public async void TestClientInitialize() {
            var client = Factory.CreateClient();
            var request = GenerateMessage(HttpMethod.Get, "/");
            var response = await client.SendAsync(request);
            Assert.False(response.IsSuccessStatusCode);
        }
    }
}
