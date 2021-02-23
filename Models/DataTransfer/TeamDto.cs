using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataTransfer
{
    public class TeamDto
    {
        [DisplayName("Team ID")]
        public Guid TeamID { get; set; }
        [DisplayName("Team Name")]
        public string Name { get; set; }
    }
}
