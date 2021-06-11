using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class SecurityClearance
    {
        public int SecurityClearanceId { get; set; }
        public string SecurityClearanceName { get; set; }
        public List<AgencyAgent> agencyAgents{ get; set; }

      //  SecurityClearance secClear = new SecurityClearance() { SecurityClearanceId = 1, SecurityClearanceName = "Eagle" };
    }
}
