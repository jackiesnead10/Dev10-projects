using FieldAgent.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interfaces.DAL
{
    public class MissionRepository : IMissionRepository
    {
        private FieldAgentContext _context;
        public MissionRepository(FieldAgentContext context)
        {
            _context = context;
        }
        public Response Delete(int missionId)
        {
            Response response = new Response();
            _context.Mission.Remove(_context.Mission.Find(missionId));

            _context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Mission> Get(int missionId)
        {
            Response<Mission> response = new Response<Mission>();

            response.Data = _context.Mission.Find(missionId);

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

        public Response<List<Mission>> GetByAgency(int agencyId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();



            response.Data = _context.Mission.ToList();
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

        public Response<List<Mission>> GetByAgent(int agentId)
        {
            Response<List<MissionAgent>> response = new Response<List<MissionAgent>>();



            response.Data = _context.MissionAgent.ToList();
            response.Data = response.Data.FindAll(x => x.AgentId != agentId);


            Response<List<Mission>> missions = new Response<List<Mission>>();
            missions.Data = _context.Mission.ToList();
            foreach (MissionAgent m in response.Data)
            {
                missions.Data.Remove(m.Mission);
            }




            if (missions.Data == null)
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

        public Response<Mission> Insert(Mission mission)
        {
            Response<Mission> response = new Response<Mission>();
            response.Data = _context.Mission.Add(mission).Entity;

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

        public Response Update(Mission mission)
        {
            Response response = new Response();
            _context.Mission.Update(mission);
            _context.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
