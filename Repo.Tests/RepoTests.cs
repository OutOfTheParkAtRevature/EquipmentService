using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

using Repository;
using Models;
namespace Repository.Tests
{
    public class RepoTests {
        [Fact]
        public async void TestGetEquipmentRequests() {
            var opt = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("get-reqs")
                .Options;
            var ctx = new EquipmentContext(opt);
            var repo = new Repo(ctx, null);
            IEnumerable<EquipmentRequest> ereqs = await repo.GetEquipmentRequests();
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
            EquipmentRequest req = await repo.GetEquipmentRequestById(new Guid());
            Assert.Null(req);
        }
        [Fact]
        public async void TestGetEquipmentItems() {
            var opt = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("get-items")
                .Options;
            var ctx = new EquipmentContext(opt);
            var repo = new Repo(ctx, null);
            IEnumerable<EquipmentItem> eitems = await repo.GetEquipmentItems();
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
            EquipmentItem item = await repo.GetEquipmentItemById(0);
            Assert.Null(item);
        }
    }
}
