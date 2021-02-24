using EquipmentService.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using Models.DataTransfer;
using Moq;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EquipmentService.Tests
{
    public class ControllerTests
    {

        /// <summary>
        /// Tests the GetEquipmentRequests() method of EquipmentController
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentRequests()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3ControllerGetEquipmentRequests")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                Mapper m = new Mapper();
                EquipmentController controller = new EquipmentController(l, m);
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

                //var getRequests = await controller.GetEquipmentRequests();
                //var convertedList = (List<EquipmentRequest>)getRequests;

                //Assert.Equal("fred", convertedList[0].UserID);
                //Assert.Equal("i need equipment now!", convertedList[0].Message);
                //Assert.Equal(53, convertedList[0].ItemId);
                //Assert.Equal("Approved", convertedList[0].Status);
                //Assert.Equal("tom", convertedList[1].UserID);
                //Assert.Equal("broken helmet", convertedList[1].Message);
                //Assert.Equal(27, convertedList[1].ItemId);
                //Assert.Equal("Pending", convertedList[1].Status);
            }
        }

        /// <summary>
        /// Tests the CreateEquipmentRequest() method of EquipmentController
        /// </summary>
        [Fact]
        public async void TestForCreateEquipmentRequest()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3ControllerCreateRequest")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                Mapper m = new Mapper();
                EquipmentController controller = new EquipmentController(l, m);
                var request = new CreateEquipmentRequestDto
                {
                    TeamID = Guid.NewGuid(),
                    UserID = "fred",
                    Message = "i need equipment now!",
                    Status = "Approved",
                    ItemID = 53,
                };

                var createRequest = await controller.CreateEquipmentRequest(request);

                Assert.Equal("fred", createRequest.Value.UserID);
                Assert.Equal("i need equipment now!", createRequest.Value.Message);
                Assert.Equal(53, createRequest.Value.ItemId);
                Assert.Equal("Approved", createRequest.Value.Status);
            }
        }

        /// <summary>
        /// Tests the EditEquipmentRequest() method of EquipmentController
        /// </summary>
        [Fact]
        public async void TestForEditEquipmentRequest()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3ControllerEditRequest")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                Mapper m = new Mapper();
                EquipmentController controller = new EquipmentController(l, m);
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

                var editRequest = await controller.EditEquipmentRequest(request.RequestID, editRequestDto);

                Assert.Equal("fred", editRequest.Value.UserID);
                Assert.Equal("i need equipment now!", editRequest.Value.Message);
                Assert.Equal(53, editRequest.Value.ItemId);
                Assert.Equal("Pending", editRequest.Value.Status);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItems() method of EquipmentController
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItems()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3ControllerGetItems")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                Mapper m = new Mapper();
                EquipmentController controller = new EquipmentController(l, m);
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

                var getItems = await controller.GetEquipmentItems();
                var convertedList = (List<EquipmentItem>)getItems;

                Assert.Equal("golf club", convertedList[0].Description);
                Assert.Equal("baseball glove", convertedList[1].Description);
            }
        }

        /// <summary>
        /// Tests the GetEquipmentItemById() method of EquipmentController
        /// </summary>
        [Fact]
        public async void TestForGetEquipmentItemById()
        {
            var options = new DbContextOptionsBuilder<EquipmentContext>()
            .UseInMemoryDatabase(databaseName: "p3ControllerGetItemById")
            .Options;

            using (var context = new EquipmentContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());
                Mapper m = new Mapper();
                EquipmentController controller = new EquipmentController(l, m);
                var item = new EquipmentItem
                {
                    EquipmentID = 43,
                    Description = "golf club"
                };
                r.EquipmentItems.Add(item);
                await r.CommitSave();

                var getItem = await controller.GetEquipmentItemById(item.EquipmentID);

                Assert.Equal("golf club", getItem.Value.Description);
            }
        }
    }
}
