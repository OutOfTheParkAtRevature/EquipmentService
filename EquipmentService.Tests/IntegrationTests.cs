using System;
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
        [Fact]
        public void TestClientInitialize() {
            var client = Factory.CreateClient();
            Assert.NotNull(client);
        }
    }
}
