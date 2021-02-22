using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DataTransfer;
using Service;

namespace EquipmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly Logic _logic;

        public EquipmentController(Logic logic)
        {
            _logic = logic;
        }
        [HttpGet]
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await _logic.GetEquipmentRequests();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentRequest>> GetEquipmentRequest(Guid id)
        {
            return await _logic.GetEquipmentRequestById(id);
        }
        [HttpPost]
        public async Task<ActionResult<EquipmentRequest>> CreateEquipmentRequest(CreateEquipmentRequestDto equipmentRequest)
        {
            return await _logic.CreateEquipmentRequest(equipmentRequest);
        }
        [HttpPut("edit/{id}")]
        public async Task<ActionResult<EquipmentRequest>> EditEquipmentRequest(Guid id, EditEquipmentRequestDto equipmentRequest)
        {
            return await _logic.EditEquipmentRequest(id, equipmentRequest);
        }
        [HttpGet("items")]
        public async Task<IEnumerable<EquipmentItem>> GetEquipmentItems()
        {
            return await _logic.GetEquipmentItems();
        }
        [HttpGet("items/{id}")]
        public async Task<ActionResult<EquipmentItem>> GetEquipmentItemById(int id)
        {
            return await _logic.GetEquipmentItemtById(id);
        }

        //Probably need things like get by user id, get by team, ect

    }
}
