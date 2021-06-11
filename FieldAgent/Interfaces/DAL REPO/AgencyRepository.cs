using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interfaces.DAL
{

    public class AgencyRepository : IAgencyRepository
    {
        private FieldAgentContext _context;
        public AgencyRepository(FieldAgentContext context)
        {
            _context = context;
        }
        public Response Delete(int agencyId)
        {
            Response response = new Response();
            _context.Agency.Remove(_context.Agency.Find(agencyId));
           
            _context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Agency> Get(int agencyId)
        {
            Response<Agency> response = new Response<Agency>();

            response.Data = _context.Agency.Find(agencyId);

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

        public Response<List<Agency>> GetAll()
        {
            Response<List<Agency>> response = new Response<List<Agency>>();
            response.Data = _context.Agency.ToList();
            
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

        public Response<Agency> Insert(Agency agency)
        {
            Response<Agency> response = new Response<Agency>();
            response.Data = _context.Agency.Add(agency).Entity;

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

        public Response Update(Agency agency)
        {
            Response response = new Response();
            _context.Agency.Update(agency);
            _context.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
