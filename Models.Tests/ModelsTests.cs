using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

using Models;
using Models.DataTransfer;

namespace Models.Tests {
    public class ModelsTests {
        [Fact]
        public void TestValidateEquipmentItem() {
            EquipmentItem item = new EquipmentItem();
            ValidationContext ctx = new ValidationContext(item);
            List<ValidationResult> results = new List<ValidationResult>();

            bool valid = Validator.TryValidateObject(item, ctx, results, true);
            Assert.True(valid);
        }
        [Fact]
        public void TestValidateEquipmentRequest() {
            EquipmentRequest request = new EquipmentRequest();
            ValidationContext ctx = new ValidationContext(request);
            List<ValidationResult> results = new List<ValidationResult>();

            bool valid = Validator.TryValidateObject(request, ctx, results, true);
            Assert.True(valid);
        }
        [Fact]
        public void TestValidateCreateEquipmentRequestDto() {
            CreateEquipmentRequestDto dto = new CreateEquipmentRequestDto();
            ValidationContext ctx = new ValidationContext(dto);
            List<ValidationResult> results = new List<ValidationResult>();

            bool valid = Validator.TryValidateObject(dto, ctx, results, true);
            Assert.True(valid);
        }
        [Fact]
        public void TestValidateEditEquipmentRequestDto() {
            EditEquipmentRequestDto dto = new EditEquipmentRequestDto();
            ValidationContext ctx = new ValidationContext(dto);
            List<ValidationResult> results = new List<ValidationResult>();

            bool valid = Validator.TryValidateObject(dto, ctx, results, true);
            Assert.True(valid);
        }
    }
}
