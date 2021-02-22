using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

using Service;
using Repository;
using Models;
using Models.DataTransfer;

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
        [Fact]
        public async void TestCreateEquipmentRequest() {
            var opt = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("create-req")
                .Options;
            var ctx = new EquipmentContext(opt);
            var repo = new Repo(ctx, null);
            var logic = new Logic(repo, null);
            CreateEquipmentRequestDto create = new CreateEquipmentRequestDto() {
                UserID = "Tyler Cadena",
                ItemID = 1,
                Message = "lorem ipsum",
                Status = "GOOD",
                TeamID = Guid.NewGuid()
            };
            EquipmentRequest req = await logic.CreateEquipmentRequest(create);
            Assert.NotNull(req);
        }
        [Fact]
        public async void TestEditEquipmentRequest() {
            var opt = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("edit-req")
                .Options;
            var ctx = new EquipmentContext(opt);
            var repo = new Repo(ctx, null);
            var logic = new Logic(repo, null);
            CreateEquipmentRequestDto create = new CreateEquipmentRequestDto() {
                UserID = "Tyler Cadena",
                ItemID = 1,
                Message = "lorem ipsum",
                Status = "GOOD",
                TeamID = Guid.NewGuid()
            };
            EquipmentRequest req1 = await logic.CreateEquipmentRequest(create);
            Assert.Equal(create.Status, req1.Status);
            EditEquipmentRequestDto edit = new EditEquipmentRequestDto() {
                Status = "BAD"
            };
            EquipmentRequest req2 = await logic.EditEquipmentRequest(req1.RequestID, edit);
            Assert.Equal(edit.Status, req2.Status);
        }
        [Fact]
        public async void TestGetEquipmentItemByName() {
            var opt = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("get-req-by-name")
                .Options;
            var ctx = new EquipmentContext(opt);
            var repo = new Repo(ctx, null);
            var logic = new Logic(repo, null);
            EquipmentItem item = await logic.GetEquipmentItemByName("does-not-exist");
            Assert.Null(item);
        }
    }
}
