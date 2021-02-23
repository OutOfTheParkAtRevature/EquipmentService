using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataTransfer;
using Models;

namespace Service
{
    public class Mapper
    {
        public EquipmentRequestDto ConvertEquipmentRequestToEquipmentRequestDto(EquipmentRequest equipment)
        {
            EquipmentRequestDto convertedRequest = new EquipmentRequestDto()
            {
                RequestID = equipment.RequestID,
                UserID = equipment.UserID,
                TeamID = equipment.TeamID,
                RequestDate = equipment.RequestDate,
                Message = equipment.Message,
                ItemId = equipment.ItemId,
                Status = equipment.Status
            };
            return convertedRequest;
        }
    }
}
