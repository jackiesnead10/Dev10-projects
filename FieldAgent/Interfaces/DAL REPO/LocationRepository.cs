using FieldAgent.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interfaces.DAL
{
    public class LocationRepository : ILocationRepository
    {
        private FieldAgentContext _context;
        public LocationRepository(FieldAgentContext context)
        {
            _context = context;
        }
        public Response Delete(int locationId)
        {
            Response response = new Response();
            _context.Location.Remove(_context.Location.Find(locationId));

            _context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Location> Get(int locationId)
        {
            Response<Location> response = new Response<Location>();

            response.Data = _context.Location.Find(locationId);

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

        public Response<List<Location>> GetByAgency(int agencyId)
        {
            Response<List<Location>> response = new Response<List<Location>>();



            response.Data = _context.Location.ToList();
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

        public Response<Location> Insert(Location location)
        {
            Response<Location> response = new Response<Location>();
            response.Data = _context.Location.Add(location).Entity;

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

        public Response Update(Location location)
        {
            Response response = new Response();
            _context.Location.Update(location);
            _context.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
