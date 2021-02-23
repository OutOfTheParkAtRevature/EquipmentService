using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DataTransfer;
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
        private readonly Mapper _mapper;

        public EquipmentController(Logic logic, Mapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<EquipmentRequestDto>> GetEquipmentRequests()
        {
            IEnumerable<EquipmentRequest> requests = await _logic.GetEquipmentRequests();
            List<EquipmentRequestDto> convertedRequests = new List<EquipmentRequestDto>();

            foreach (EquipmentRequest request in requests)
            {
                EquipmentRequestDto convert = _mapper.ConvertEquipmentRequestToEquipmentRequestDto(request);
                convertedRequests.Add(convert);
            }

            // add logic to get user, team, item

            return convertedRequests;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentRequestDto>> GetEquipmentRequest(Guid id)
        {
            EquipmentRequest request = await _logic.GetEquipmentRequestById(id);
            EquipmentRequestDto convertedRequest = _mapper.ConvertEquipmentRequestToEquipmentRequestDto(request);

            // add logic to get user, team, item

            return convertedRequest;
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
