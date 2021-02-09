﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EquipmentRequest
    {
        [Key]
        [DisplayName("Request ID")]
        public int RequestID { get; set; }
        [DisplayName("User ID")]
        public Guid UserID { get; set; }
        [DisplayName("Team ID")]
        public Guid TeamID { get; set; }
        [DisplayName("Request Date")]
        public DateTime RequestDate { get; set; }
        public string Message { get; set; }
        public int ItemID { get; set; }
        public string Status { get; set; }
    }
}
