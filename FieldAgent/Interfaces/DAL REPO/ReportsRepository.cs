using FieldAgent.Core.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interfaces.DAL
{
    public class ReportsRepository : IReportsRepository
    {
        private FieldAgentContext _context;
       public ReportsRepository(FieldAgentContext context)
       {
           _context = context;
            
       }
        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            List<ClearanceAuditListItem> list = new List<ClearanceAuditListItem>();
            ClearanceAuditListItem item;
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<FieldAgentContext>();
            var config = builder.Build();

            string _connectionString = config["ConnectionStrings:FieldAgent"];


            using (var cn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAuditList", cn);
                cmd.Parameters.AddWithValue("@agencyId", agencyId);
                cmd.Parameters.AddWithValue("@securityClearanceId", securityClearanceId);

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item = new ClearanceAuditListItem();
                        Console.WriteLine($" {dr["BadgeId"]} {dr["FullName"]} {dr["DateOfBirth"]} {dr["ActivationDate"]} {dr["DeactivationDate"]} ");
                        item.NameLastFirst = dr["FullName"].ToString();
                        item.DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString());
                        item.ActivationDate = DateTime.Parse(dr["ActivationDate"].ToString());
                        item.DeactivationDate = DateTime.Parse(dr["DeactivationDate"].ToString());
                        item.BadgeId = Guid.Parse(dr["BadgeId"].ToString());


                        list.Add(item);
                    }
                }
            }
            Response<List<ClearanceAuditListItem>> response = new Response<List<ClearanceAuditListItem>>();
            response.Data = list;
            return response;
        }
    

        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            List<PensionListItem> list = new List<PensionListItem>();
            PensionListItem item;
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<FieldAgentContext>();
            var config = builder.Build();

            string _connectionString = config["ConnectionStrings:FieldAgent"];


            using (var cn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetPension", cn);
                cmd.Parameters.AddWithValue("@AgencyId",agencyId);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item = new PensionListItem();
                        Console.WriteLine($" {dr["LongName"]} {dr["BadgeId"]} {dr["FullName"]} {dr["DateOfBirth"]} {dr["DeactivationDate"]} ");
                        item.NameLastFirst = dr["FullName"].ToString();
                        item.DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString());
                        item.AgencyName = (dr["LongName"].ToString());
                        item.DeactivationDate= DateTime.Parse(dr["DeactivationDate"].ToString());
                        item.BadgeId = Guid.Parse(dr["BadgeId"].ToString());


                        list.Add(item);
                    }
                }
            }
            Response<List<PensionListItem>> response = new Response<List<PensionListItem>>();
            response.Data = list;
            return response;
        }
    

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            List<TopAgentListItem> list = new List<TopAgentListItem>();
            TopAgentListItem item;
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<FieldAgentContext>();
            var config = builder.Build();

            string _connectionString = config["ConnectionStrings:FieldAgent"];


            using (var cn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetTopAgents", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item = new TopAgentListItem();
                        Console.WriteLine($"{dr["AgentId"]} {dr["FullName"]} {dr["DateOfBirth"]} {dr["CompletedMissions"]} ");
                        item.NameLastFirst = dr["FullName"].ToString();
                        item.DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString());
                        item.CompletedMissionCount = int.Parse(dr["CompletedMissions"].ToString());
                        list.Add(item);
                    }
                }
            }
            Response<List<TopAgentListItem>> response = new Response<List<TopAgentListItem>>();
            response.Data = list;
            return response;
        }
    }
}
