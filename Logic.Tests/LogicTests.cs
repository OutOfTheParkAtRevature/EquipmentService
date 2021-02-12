using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

using Service;
using Repository;
using Models;

namespace Service.Tests {
    public class LogicTests {
        [Fact]
        public async void TestGetEquipmentRequests() {
            var opt = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("get-reqs")
                .Options;
            var ctx = new EquipmentContext(opt);
            var repo = new Repo(ctx, null);
            var logic = new Logic(repo, null);
            IEnumerable<EquipmentRequest> ereqs = await logic.GetEquipmentRequests();
            List<EquipmentRequest> lreqs = ereqs.ToList();
            Assert.Empty(lreqs);
        }
        [Fact]
        public async void TestGetEquipmentRequestById() {
            var opt = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("get-reqs-id")
                .Options;
            var ctx = new EquipmentContext(opt);
            var repo = new Repo(ctx, null);
            var logic = new Logic(repo, null);
            EquipmentRequest req = await logic.GetEquipmentRequestById(new Guid());
            Assert.Null(req);
        }
        [Fact]
        public async void TestGetEquipmentItems() {
            var opt = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("get-items")
                .Options;
            var ctx = new EquipmentContext(opt);
            var repo = new Repo(ctx, null);
            var logic = new Logic(repo, null);
            IEnumerable<EquipmentItem> eitems = await logic.GetEquipmentItems();
            List<EquipmentItem> litems = eitems.ToList();
            Assert.Empty(litems);
        }
        [Fact]
        public async void TestGetEquipmentItemById() {
            var opt = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("get-items-id")
                .Options;
            var ctx = new EquipmentContext(opt);
            var repo = new Repo(ctx, null);
            var logic = new Logic(repo, null);
            EquipmentItem item = await logic.GetEquipmentItemtById(0);
            Assert.Null(item);
        }
    }
}
