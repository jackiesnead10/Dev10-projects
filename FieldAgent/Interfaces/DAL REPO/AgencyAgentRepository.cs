using FieldAgent.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interfaces.DAL
{
    public class AgencyAgentRepository : IAgencyAgentRepository
    {
        private FieldAgentContext _context;

        public AgencyAgentRepository(FieldAgentContext context)
        {
            _context = context;
        }

        public Response Delete(int agencyid, int agentid)
        {
            Response response = new Response();
             _context.Agency.Remove(_context.Agency.Find(agencyid));
            _context.Agent.Remove(_context.Agent.Find(agentid));
                _context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {

            Response<AgencyAgent> response = new Response<AgencyAgent>();
            response.Data = _context.AgencyAgent.Find(agencyid, agentid);
            if(response.Data == null)
            {
                response.Success = false;
                response.Message = "Error: could not add";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();

            response.Data = _context.AgencyAgent.ToList();
            response.Data = response.Data.FindAll(x => x.AgencyId == agencyId);

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

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();

            response.Data = _context.AgencyAgent.ToList();
            response.Data = response.Data.FindAll(x => x.AgentId == agentId);
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

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            Response<AgencyAgent> response = new Response<AgencyAgent>();
           response.Data =  _context.AgencyAgent.Add(agencyAgent).Entity;

            if (response.Data == null)
            {
                response.Success = false;
                response.Message = "Error: could not add";
            }
            else
            {
                _context.SaveChanges();
                response.Success = true;
            }
           
            return response;
        }

        public Response Update(AgencyAgent agencyAgent)
        {
            Response response = new Response();
            _context.AgencyAgent.Update(agencyAgent);
            _context.SaveChanges();
            response.Success = true;
            return response;
            
        }
    }
}

