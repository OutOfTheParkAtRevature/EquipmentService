using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataTransfer
{
    public class ItemDto
    {
        [DisplayName("Equipment ID")]
        public int EquipmentID { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
