using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

using Models;
using Models.DataTransfer;
using Model.DataTransfer;

namespace Models.Tests {
    public class ModelsTests {

        /// <summary>
        /// Checks the data annotations of Models to make sure they aren't being violated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IList<ValidationResult> ValidateModel(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);
            // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }

        /// <summary>
        /// Makes sure EquipmentItem model works with valid data
        /// </summary>
        [Fact]
        public void ValidateEquipmentItem()
        {
            var item = new EquipmentItem
            {
                EquipmentID = 43,
                Description = "golf club"
            };

            var results = ValidateModel(item);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure EquipmentRequest model works with valid data
        /// </summary>
        [Fact]
        public void ValidateEquipmentRequest()
        {
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

            var results = ValidateModel(request);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure CreateEquipmentRequestDto model works with valid data
        /// </summary>
        [Fact]
        public void ValidateCreateEquipmentRequestDto()
        {
            var requestDto = new CreateEquipmentRequestDto
            {
                TeamID = Guid.NewGuid(),
                UserID = "fred",
                Message = "i need equipment now!",
                ItemID = 53,
                Status = "Approved"
            };

            var results = ValidateModel(requestDto);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure EditEquipmentRequestDto model works with valid data
        /// </summary>
        [Fact]
        public void ValidateEditEquipmentRequestDto()
        {
            var requestDto = new EditEquipmentRequestDto
            {
                Status = "Approved"
            };

            var results = ValidateModel(requestDto);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure EquipmentRequestDto model works with valid data
        /// </summary>
        [Fact]
        public void ValidateEquipmentRequestDto()
        {
            var requestDto = new EquipmentRequestDto
            {
                RequestID = Guid.NewGuid(),
                TeamID = Guid.NewGuid(),
                User = new UserDto(),
                UserID = "fred",
                Team = new TeamDto(),
                RequestDate = DateTime.Now,
                Message = "i need equipment now!",
                Item = new EquipmentItem(),
                ItemId = 53,
                Status = "Approved"
            };

            var results = ValidateModel(requestDto);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure ItemDto model works with valid data
        /// </summary>
        [Fact]
        public void ValidateItemDto()
        {
            var item = new ItemDto
            {
                EquipmentID = 43,
                Description = "golf club"
            };

            var results = ValidateModel(item);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure TeamDto model works with valid data
        /// </summary>
        [Fact]
        public void ValidateTeamDto()
        {
            var team = new TeamDto
            {
                TeamID = Guid.NewGuid(),
                Name = "Buffalos"
            };

            var results = ValidateModel(team);
            Assert.True(results.Count == 0);
        }

        /// <summary>
        /// Makes sure UserDto model works with valid data
        /// </summary>
        [Fact]
        public void ValidateUserDto()
        {
            var user = new UserDto
            {
                Id = "25",
                UserName = "georgie",
                FullName = "George RR Martin",
                PhoneNumber = "222-222-2222",
                Email = "himom.com",
                TeamID = Guid.NewGuid(),
                RoleName = "Player"
            };

            var results = ValidateModel(user);
            Assert.True(results.Count == 1);
        }
    }
}
