using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interfaces.DAL
{
    public class FieldAgentContext : DbContext
    {
        public DbSet<Agency> Agency { get; set; }
        public DbSet<AgencyAgent> AgencyAgent { get; set; }

        public DbSet<Agent> Agent { get; set; }
        public DbSet<Alias> Alias { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<MissionAgent> MissionAgent { get; set; }
        public DbSet<SecurityClearance> SecurityClearance { get; set; }




      //  builder.AddUserSecrets<FieldAgentContext>();



        public FieldAgentContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Other configuration left out to focus on many-to-many
            modelBuilder.Entity<AgencyAgent>()
                .HasKey(table => new { table.AgencyId, table.AgentId });

           

           ////  .HasKey(table => new { table.SecurityClearanceId, ta });
            //check on below if causes issues in test
            modelBuilder.Entity<AgencyAgent>()
                .HasOne(x => x.Agency);
            modelBuilder.Entity<AgencyAgent>().HasOne(x => x.Agent);



            modelBuilder.Entity<MissionAgent>()
                  .HasKey(table => new { table.MissionId, table.AgentId });

          

            // modelBuilder.Entity<Agent>()
            //    .HasMany(x => x.Alias);






            /* modelBuilder.Entity<Customer>()
                 .HasMany(x => x.Projects)
                 .WithOne(x => x.PrimaryCustomer);
             modelBuilder.Entity<Project>()
                 .HasOne(x => x.PrimaryCustomer)
                 .WithMany(x => x.Projects);
             modelBuilder.Entity<Employee>()
                         .HasMany(x => x.Projects)
                         .WithMany(x => x.Employees)
                          .UsingEntity<Dictionary<string, object>>(
                                 "ProjectEmployee", // <-- Name of the table
                                 j => j
                                     .HasOne<Project>() // <-- from the bridget table, it has one project
                                     .WithMany() // <-- that can have many entries in the bridge table
                                     .HasForeignKey("ProjectId") // <-- it's column name is ProjectId
                                 ,
                                 j => j
                                     .HasOne<Employee>()
                                     .WithMany()
                                     .HasForeignKey("EmployeeId")
                             );*/


        }
       /* public static FieldAgentContext GetDBContext()
        {
            var options = new DbContextOptionsBuilder<FieldAgentContext>()
        .UseSqlServer("Server=localhost;Database=FieldAgent;User Id=sa;Password=YOUR_strong_*pass4w0rd*")
        .Options;
            return new FieldAgentContext(options);
        }*/

        public static FieldAgentContext GetDbContext()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<FieldAgentContext>();
            var config = builder.Build();
            var connectionString = config["ConnectionStrings:FieldAgent"];
            var options = new DbContextOptionsBuilder<FieldAgentContext>()
                .UseSqlServer(connectionString)
                .Options;
            return new FieldAgentContext(options);
        }


    }
}
