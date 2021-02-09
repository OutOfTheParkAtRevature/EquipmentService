using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class EquipmentItem
    {
        [Key]
        [DisplayName("Equipment ID")]
        public int EquipmentId { get; set; }
        public string Description { get; set; }
    }
}
