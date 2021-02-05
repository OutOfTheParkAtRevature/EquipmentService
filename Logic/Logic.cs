using System;

namespace Logic
{
    public class Logic
    {
        /// <summary>
        /// Get an EquipmentRequest by ID
        /// </summary>
        /// <param name="id">EquipmentRequestID</param>
        /// <returns>EquipmentRequest</returns>
        public async Task<EquipmentRequest> GetEquipmentRequestById(int id)
        {
            return await _repo.GetEquipmentRequestById(id);
        }
        /// <summary>
        /// Get a list of EquipmentRequests
        /// </summary>
        /// <returns>list of EquipmentRequests</returns>
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await _repo.GetEquipmentRequests();
        }
        /// <summary>
        /// Get EquipmentRequest by ID
        /// </summary>
        /// <param name="id">EquipmentRequestID</param>
        /// <returns>EquipmentRequest</returns>
        public async Task<EquipmentItem> GetEquipmentItemtById(int id)
        {
            return await _repo.GetEquipmentItemById(id);
        }
        /// <summary>
        /// Get list of EquipmentItems
        /// </summary>
        /// <returns>list of EquipmentRequests</returns>
        public async Task<IEnumerable<EquipmentItem>> GetEquipmentItems()
        {
            return await _repo.GetEquipmentItems();
        }
        /// <summary>
        /// Create new EquipmentRequest
        /// </summary>
        /// <param name="createEquipmentRequestDto">EquipmentRequest from input</param>
        /// <returns>EquipmentRequest</returns>
        public async Task<EquipmentRequest> CreateEquipmentRequest(CreateEquipmentRequestDto createEquipmentRequestDto)
        {
            EquipmentRequest newEquipmentRequest = new EquipmentRequest()
            {
                UserID = createEquipmentRequestDto.UserID,
                TeamID = createEquipmentRequestDto.TeamID,
                RequestDate = DateTime.Now,
                Message = createEquipmentRequestDto.Message,
                ItemId = createEquipmentRequestDto.ItemID,
                Status = createEquipmentRequestDto.Status
            };
            await _repo.equipmentRequests.AddAsync(newEquipmentRequest);
            await _repo.CommitSave();
            return newEquipmentRequest;
        }
        /// <summary>
        /// Edit an EquipmentRequest
        /// </summary>
        /// <param name="id">EqupmentRequestID</param>
        /// <param name="editEquipmentRequestDto">new information from input</param>
        /// <returns>modified EquipmentRequest</returns>
        public async Task<EquipmentRequest> EditEquipmentRequest(int id, EditEquipmentRequestDto editEquipmentRequestDto)
        {
            EquipmentRequest editedEquipmentRequest = await GetEquipmentRequestById(id);
            if (editedEquipmentRequest != null && editedEquipmentRequest.Status != editEquipmentRequestDto.Status) { editedEquipmentRequest.Status = editEquipmentRequestDto.Status; }
            await _repo.CommitSave();
            return editedEquipmentRequest;
        }
        /// <summary>
        /// Get an EquipmentItem by Name
        /// </summary>
        /// <param name="eqName">Equipment Name</param>
        /// <returns>EquipmentItem</returns>
        public async Task<EquipmentItem> GetEquipmentItemByName(string eqName)
        {
            return await _repo.equipmentItems.FirstOrDefaultAsync(x => x.Description == eqName);
        }
    }
}
