using FieldAgent.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interfaces.DAL
{
    public class AliasRepository : IAliasRepository
    {
        private FieldAgentContext _context;
        public AliasRepository(FieldAgentContext context)
        {
            _context = context;
        }
        Agency agency = new Agency();
        public Response Delete(int aliasId)
        {
            Response response = new Response();
            _context.Alias.Remove(_context.Alias.Find(aliasId));

            _context.SaveChanges();
            response.Success = true;
            return response;

        }

        public Response<Alias> Get(int aliasId)
        {
            Response<Alias> response = new Response<Alias>();

            response.Data = _context.Alias.Find(aliasId);

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

        public Response<List<Alias>> GetByAgent(int agentId)
        {
            Response<List<Alias>> response = new Response<List<Alias>>();
            


            response.Data = _context.Alias.ToList();
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

        public Response<Alias> Insert(Alias alias)
        {
            Response<Alias> response = new Response<Alias>();
            response.Data = _context.Alias.Add(alias).Entity;

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

        public Response Update(Alias alias)
        {
            Response response = new Response();
            _context.Alias.Update(alias);
            _context.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
