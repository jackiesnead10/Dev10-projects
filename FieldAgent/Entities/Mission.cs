using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class Mission
    {
        public int MissionId { get; set; }
        public int AgencyId { get; set; }
        public string CodeName { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public Decimal OperationalCost { get; set; }

        public Agency Agency { get; set; }

        public List<MissionAgent> MissionAgents { get; set; }


    }
}
