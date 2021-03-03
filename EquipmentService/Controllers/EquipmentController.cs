using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DataTransfer;
using Models;
using Models.DataTransfer;
using Newtonsoft.Json;
using Service;

namespace EquipmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

            var token = await HttpContext.GetTokenAsync("access_token");
            using (var httpClient = new HttpClient()) { 
                   
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                foreach (EquipmentRequest request in requests)
                {
                    EquipmentRequestDto convert = _mapper.ConvertEquipmentRequestToEquipmentRequestDto(request);
                    var response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{request.TeamID}");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var team = JsonConvert.DeserializeObject<TeamDto>(apiResponse);
                    convert.Team = team;

                    response = await httpClient.GetAsync($"http://10.0.167.177/api/User/{request.UserID}");
                    apiResponse = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<UserDto>(apiResponse);
                    convert.User = user;

                    EquipmentItem item = await _logic.GetEquipmentItemById(request.ItemId);
                    convert.Item = item;
                    convertedRequests.Add(convert);
                }
            }

            // add logic to get user, team, item

            return convertedRequests;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentRequestDto>> GetEquipmentRequest(Guid id)
        {
            EquipmentRequest request = await _logic.GetEquipmentRequestById(id);
            EquipmentRequestDto convertedRequest = _mapper.ConvertEquipmentRequestToEquipmentRequestDto(request);

            var token = await HttpContext.GetTokenAsync("access_token");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{request.TeamID}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var team = JsonConvert.DeserializeObject<TeamDto>(apiResponse);
                convertedRequest.Team = team;

                response = await httpClient.GetAsync($"http://10.0.167.177/api/User/{request.UserID}");
                apiResponse = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDto>(apiResponse);
                convertedRequest.User = user;

                EquipmentItem item = await _logic.GetEquipmentItemById(request.ItemId);
                convertedRequest.Item = item;
            }

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
            return await _logic.GetEquipmentItemById(id);
        }

        //Probably need things like get by user id, get by team, ect

    }
}
