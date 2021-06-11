using FieldAgent.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interfaces.DAL
{
    public class AgentRepository : IAgentRepository
    {
        private FieldAgentContext _context;
        public AgentRepository(FieldAgentContext context)
        {
            _context = context;
        }
        MissionAgent missionAgent = new MissionAgent();
        public Response Delete(int agentId)
        {
            Response response = new Response();
            _context.Agent.Remove(_context.Agent.Find(agentId));

            _context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Agent> Get(int agentId)
        {
            Response<Agent> response = new Response<Agent>();

            response.Data = _context.Agent.Find(agentId);

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

        Response<List<Mission>> IAgentRepository.GetMissions(int agentId)
        {
            Response<List<MissionAgent>> response = new Response<List<MissionAgent>>();
            response.Data = _context.MissionAgent.ToList();
            response.Data = response.Data.FindAll(x => x.AgentId != agentId);

            Response<List<Mission>> missions = new Response<List<Mission>>();
            missions.Data = _context.Mission.ToList();
            foreach(MissionAgent m in response.Data)
            {
                missions.Data.Remove(m.Mission);
            }
            
            

            if (missions.Data.Count == 0 || missions.Data == null)
            {
                missions.Success = false;
                missions.Message = "Error: could not find";
            }
            else
            {
                missions.Success = true;
            }


            return missions;


        }

        public Response<Agent> Insert(Agent agent)
        {
            Response<Agent> response = new Response<Agent>();
            response.Data = _context.Agent.Add(agent).Entity;

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

        public Response Update(Agent agent)
        {
            Response response = new Response();
            _context.Agent.Update(agent);
            _context.SaveChanges();
            response.Success = true;
            return response;
        }

      
    }
}
       

