using FieldAgent.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interfaces.DAL
{
    public class SecurityClearanceRepository : ISecurityClearanceRepository
    {
        private FieldAgentContext _context;
        public SecurityClearanceRepository(FieldAgentContext context)
        {
            _context = context;
        }
        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            Response<SecurityClearance> response = new Response<SecurityClearance>();

            response.Data = _context.SecurityClearance.Find(securityClearanceId);

            if (response.Data == null)
            {
                response.Success = false;
                response.Message = "Error: could not find";
            }
            else
            {
                response.Success = true;
            }


            return response;
        }

        public Response<List<SecurityClearance>> GetAll()
        {
            Response<List<SecurityClearance>> response = new Response<List<SecurityClearance>>();
            response.Data = _context.SecurityClearance.ToList();

            if (response.Data == null)
            {
                response.Success = false;
                response.Message = "Error: could not find";
            }
            else
            {
                response.Success = true;
            }


            return response;

        }
    }
}
