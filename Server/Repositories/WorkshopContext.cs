using Microsoft.EntityFrameworkCore;
using ModelProvider;
using ModelProvider.Models;
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

        public DbSet<Bonus> Bonuses { get; set; }
        public DbSet<Repair> Repairs { get; set; } 
        public DbSet<RepairLog> RepairLogs { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RepairTechnician> RepairTechnicians { get; set; }
        public DbSet<BonusRepair> BonusRepairs { get; set; }

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
                .HasForeignKey(job => job.RepairID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RepairTechnician>().HasOne(job => job.Technician)
               .WithMany(jt => jt.RepairTechnicians)
               .HasForeignKey(job => job.TechnicianId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Repair>().HasKey(r => r.Id);
            modelBuilder.Entity<BonusRepair>().HasKey(k => new { k.RepairID, k.BonusName });
            modelBuilder.Entity<BonusRepair>().HasOne(job => job.Bonus)
                .WithMany(jt => jt.BonusRepairs)
                .HasForeignKey(job => job.BonusName);
            modelBuilder.Entity<BonusRepair>().HasOne(job => job.Repair)
               .WithMany(jt => jt.BonusRepairs)
               .HasForeignKey(job => job.RepairID);

            modelBuilder.Entity<User>().HasIndex("Username").IsUnique();
            modelBuilder.Entity<Auto>().HasIndex("LicencePlate").IsUnique();
            modelBuilder.Entity<Client>().HasIndex("Email").IsUnique();
            modelBuilder.Entity<User>().HasIndex("Email").IsUnique();
        }
    }
}
