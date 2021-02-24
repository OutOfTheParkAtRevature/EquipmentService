using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

using Service;
using Repository;
using Models;
using Models.DataTransfer;
using Microsoft.Extensions.Logging.Abstractions;

namespace Service.Tests {
    public class LogicTests {

        /// <summary>
        /// Tests the GetEquipmentRequestById() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentRequestById()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetRequestById")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                var request = new EquipmentRequest
                {
                    RequestID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    UserID = "fred",
                    RequestDate = DateTime.Now,
                    Message = "i need equipment now!",
                    ItemId = 53,
                    Status = "Approved"
                };
                r.EquipmentRequests.Add(request);
                await r.CommitSave();

                var getRequest = await l.GetEquipmentRequestById(request.RequestID);

                Assert.Equal("fred", getRequest.UserID);
                Assert.Equal("i need equipment now!", getRequest.Message);
                Assert.Equal(53, getRequest.ItemId);
                Assert.Equal("Approved", getRequest.Status);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequests() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentRequests()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetEquipmentRequests")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                var request = new EquipmentRequest
                {
                    RequestID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    UserID = "fred",
                    RequestDate = DateTime.Now,
                    Message = "i need equipment now!",
                    ItemId = 53,
                    Status = "Approved"
                };
                r.EquipmentRequests.Add(request);
                var request2 = new EquipmentRequest
                {
                    RequestID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    UserID = "tom",
                    RequestDate = DateTime.Now,
                    Message = "broken helmet",
                    ItemId = 27,
                    Status = "Pending"
                };
                r.EquipmentRequests.Add(request2);
                await r.CommitSave();

                var getRequests = await l.GetEquipmentRequests();
                var convertedList = (List<EquipmentRequest>)getRequests;

                Assert.Equal("fred", convertedList[0].UserID);
                Assert.Equal("i need equipment now!", convertedList[0].Message);
                Assert.Equal(53, convertedList[0].ItemId);
                Assert.Equal("Approved", convertedList[0].Status);
                Assert.Equal("tom", convertedList[1].UserID);
                Assert.Equal("broken helmet", convertedList[1].Message);
                Assert.Equal(27, convertedList[1].ItemId);
                Assert.Equal("Pending", convertedList[1].Status);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItemById() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItemById()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetItemById")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                var item = new EquipmentItem
                {
                    EquipmentID = 43,
                    Description = "golf club"
                };
                r.EquipmentItems.Add(item);
                await r.CommitSave();

                var getItem = await l.GetEquipmentItemById(item.EquipmentID);

                Assert.Equal("golf club", getItem.Description);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItems() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItems()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetItems")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                var item = new EquipmentItem
                {
                    EquipmentID = 43,
                    Description = "golf club"
                };
                r.EquipmentItems.Add(item);
                var item2 = new EquipmentItem
                {
                    EquipmentID = 61,
                    Description = "baseball glove"
                };
                r.EquipmentItems.Add(item2);
                await r.CommitSave();

                var getItems = await l.GetEquipmentItems();
                var convertedList = (List<EquipmentItem>)getItems;

                Assert.Equal("golf club", convertedList[0].Description);
                Assert.Equal("baseball glove", convertedList[1].Description);
            }
        }

        /// <summary>
        /// Tests the CreateEquipmentRequest() method of Logic
        /// </summary>
        [Fact]
        public async void TestForCreateEquipmentRequest()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicCreateRequest")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                var request = new CreateEquipmentRequestDto
                {
                    TeamID = Guid.NewGuid(),
                    UserID = "fred",
                    Message = "i need equipment now!",
                    Status = "Approved",
                    ItemID = 53,
                };

                var createRequest = await l.CreateEquipmentRequest(request);

                Assert.Equal("fred", createRequest.UserID);
                Assert.Equal("i need equipment now!", createRequest.Message);
                Assert.Equal(53, createRequest.ItemId);
                Assert.Equal("Approved", createRequest.Status);
            }
        }

        /// <summary>
        /// Tests the EditEquipmentRequest() method of Logic
        /// </summary>
        [Fact]
        public async void TestForEditEquipmentRequest()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicEditRequest")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                var request = new EquipmentRequest
                {
                    RequestID = Guid.NewGuid(),
                    TeamID = Guid.NewGuid(),
                    UserID = "fred",
                    RequestDate = DateTime.Now,
                    Message = "i need equipment now!",
                    ItemId = 53,
                    Status = "Approved"
                };
                r.EquipmentRequests.Add(request);
                await r.CommitSave();

                var editRequestDto = new EditEquipmentRequestDto
                {
                    Status = "Pending"
                };

                var editRequest = await l.EditEquipmentRequest(request.RequestID, editRequestDto);

                Assert.Equal("fred", editRequest.UserID);
                Assert.Equal("i need equipment now!", editRequest.Message);
                Assert.Equal(53, editRequest.ItemId);
                Assert.Equal("Pending", editRequest.Status);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItemByName() method of Logic
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItemByName()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3LogicGetItemByName")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                var item = new EquipmentItem
                {
                    EquipmentID = 43,
                    Description = "golf club"
                };
                r.EquipmentItems.Add(item);
                await r.CommitSave();

                var getItem = await l.GetEquipmentItemByName(item.Description);

                Assert.Equal(43, getItem.EquipmentID);
            }
        }
    }
}
