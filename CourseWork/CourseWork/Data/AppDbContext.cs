using System;
using Microsoft.EntityFrameworkCore;
namespace CourseWork.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Repository> Repositories { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Working> Workings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.Entity<Team>()
            .HasOne(t => t.Customer)
            .WithOne()
            .HasForeignKey<Team>(x => x.CustomerId);

            modelBuilder.Entity<Team>()
            .HasOne(t => t.TeamLeader)
            .WithOne()
            .HasForeignKey<Team>(x => x.TeamLeadId);

            modelBuilder.Entity<Project>()
            .HasOne(t => t.Team)
            .WithOne()
            .HasForeignKey<Project>(x => x.TeamId);

            modelBuilder.Entity<Working>()
                .HasKey(x => new { x.TeamId, x.WorkerId });
        }
           
        
    }
}

