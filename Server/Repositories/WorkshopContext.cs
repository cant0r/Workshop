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
        public DbSet<Repair> Repairs { get; set; } 
        public DbSet<RepairLog> RepairLogs { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<RepairTechnician> RepairTechnicians { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\mssqllocaldb;Database=WorkshopDB;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepairTechnician>().HasKey(k => new { k.RepairID, k.TechnicianId });
            modelBuilder.Entity<RepairTechnician>().HasOne(job => job.Repair)
                .WithMany(jt => jt.RepairTechnicians)
                .HasForeignKey(job => job.RepairID);
            modelBuilder.Entity<RepairTechnician>().HasOne(job => job.Technician)
               .WithMany(jt => jt.RepairTechnician)
               .HasForeignKey(job => job.TechnicianId);

            modelBuilder.Entity<Technician>().HasIndex("PhoneNumber").IsUnique();
            modelBuilder.Entity<Manager>().HasIndex("PhoneNumber").IsUnique();
            modelBuilder.Entity<Client>().HasIndex("PhoneNumber").IsUnique();
            modelBuilder.Entity<Auto>().HasIndex("LicencePlate").IsUnique();
        }
    }
}
