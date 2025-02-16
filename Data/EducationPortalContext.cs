using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Model;


namespace Data
{
    public class EducationPortalContext : DbContext
    {
        //public EducationPortalContext(DbContextOptions<EducationPortalContext> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EducationPortalDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.CompletedCourses)
                .WithMany();

            modelBuilder.Entity<User>()
                .HasMany(u => u.InProgressCourses)
                .WithMany();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Materials)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSkill>()
                .HasKey(us => new { us.UserId, us.SkillId });

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.User)
                .WithMany(u => u.Skills)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.Skill)
                .WithMany()
                .HasForeignKey(us => us.SkillId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Materials);                

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Skills)
                .WithMany();
            modelBuilder.Entity<Material>()
                .HasDiscriminator<string>("MaterialType")
                .HasValue<Video>("Video")
                .HasValue<Book>("Book")
                .HasValue<Article>("Article");
        }
    }
}
