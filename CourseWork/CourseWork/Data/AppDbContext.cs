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
            modelBuilder.Entity<Task>()
                .HasOne(r => r.Repo)
                .WithMany(t => t.Tasks)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Repository>()
                .HasOne(p => p.Proj)
                .WithMany(r => r.Repositories)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Team>()
            .HasOne(t => t.Customer)
            .WithOne()
            .HasForeignKey<Team>(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Team>()
            .HasOne(t => t.TeamLeader)
            .WithOne()
            .HasForeignKey<Team>(x => x.TeamLeadId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
            .HasOne(t => t.Team)
            .WithOne()
            .HasForeignKey<Project>(x => x.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Working>()
               .HasKey(t => new { t.TeamId, t.WorkerId });

            modelBuilder.Entity<Working>()
                .HasOne(pt => pt.Team)
                .WithMany(p => p.Workings)
                .HasForeignKey(pt => pt.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Working>()
                .HasOne(pt => pt.Worker)
                .WithMany(t => t.Workings)
                .HasForeignKey(pt => pt.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}

