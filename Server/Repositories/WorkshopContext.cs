using Microsoft.EntityFrameworkCore;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repositories
{
    public class WorkshopContext : DbContext
    {

        public DbSet<Auto> Automobiles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Job> Jobs { get; set; } 
        public DbSet<JobLog> JobLogs { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<State> States { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\mssqllocaldb;Database=WorkshopDB;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobTechnician>().HasKey(k => new { k.JobId, k.TechnicianId });
            modelBuilder.Entity<JobTechnician>().HasOne(job => job.Job)
                .WithMany(jt => jt.JobTechnicians)
                .HasForeignKey(job => job.JobId);
            modelBuilder.Entity<JobTechnician>().HasOne(job => job.Technician)
               .WithMany(jt => jt.JobTechnicians)
               .HasForeignKey(job => job.TechnicianId);
        }
    }
}
