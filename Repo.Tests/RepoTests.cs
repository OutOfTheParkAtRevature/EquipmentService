using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

using Repository;
using Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace Repository.Tests
{
    public class RepoTests {

        /// <summary>
        /// Tests the CommitSave() method of Repo
        /// </summary>
        [Fact]
        public async void TestForCommitSave()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3CommitSave")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var item = new EquipmentItem
                {
                    EquipmentID = 43,
                    Description = "golf club"
                };
                r.EquipmentItems.Add(item);
                await r.CommitSave();
                Assert.NotEmpty(context.EquipmentItems);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequestById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentRequestById()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3GetRequestById")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
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

                var getRequest = await r.GetEquipmentRequestById(request.RequestID);

                Assert.Equal("fred", getRequest.UserID);
                Assert.Equal("i need equipment now!", getRequest.Message);
                Assert.Equal(53, getRequest.ItemId);
                Assert.Equal("Approved", getRequest.Status);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentRequests() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentRequests()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3GetEquipmentRequests")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
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

                var getRequests = await r.GetEquipmentRequests();
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
        /// Tests the GetEquipmentItemById() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItemById()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3GetItemById")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                var item = new EquipmentItem
                {
                    EquipmentID = 43,
                    Description = "golf club"
                };
                r.EquipmentItems.Add(item);
                await r.CommitSave();

                var getItem = await r.GetEquipmentItemById(item.EquipmentID);

                Assert.Equal("golf club", getItem.Description);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItems() method of Repo
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItems()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3GetItems")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
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

                var getItems = await r.GetEquipmentItems();
                var convertedList = (List<EquipmentItem>)getItems;

                Assert.Equal("golf club", convertedList[0].Description);
                Assert.Equal("baseball glove", convertedList[1].Description);
            }
        }
    }
}
