using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Identity;

using Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Data
{
    public class EducationPortalContext : IdentityDbContext<User>
    {
        public EducationPortalContext(DbContextOptions<EducationPortalContext> options) : base(options) { }

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CompletedCourses)
                .WithMany()
                .UsingEntity(j => j.ToTable("CompletedCourses"));


            modelBuilder.Entity<User>()
                .HasMany(u => u.InProgressCourses)
                .WithMany()
                .UsingEntity(j => j.ToTable("InProgressCourses"));


            modelBuilder.Entity<Course>()
                .HasMany(c => c.Materials)
                .WithMany();
                

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Creator)
                .WithMany()
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);

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
                .HasMany(c => c.Skills)
                .WithMany();
            modelBuilder.Entity<Material>()
                .HasDiscriminator<string>("MaterialType")
                .HasValue<Video>("Video")
                .HasValue<Book>("Book")
                .HasValue<Article>("Article");
            modelBuilder.Entity<Material>()
                .HasOne(m => m.Creator)
                .WithMany()
                .HasForeignKey(m => m.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
