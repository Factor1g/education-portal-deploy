using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;


namespace Data
{
    public class EducationPortalContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EducationPortalDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many: User <-> Course (Completed)
            modelBuilder.Entity<User>()
                .HasMany(u => u.CompletedCourses)
                .WithMany();

            // Many-to-Many: User <-> Course (InProgress)
            modelBuilder.Entity<User>()
                .HasMany(u => u.InProgressCourses)
                .WithMany();

            // One-to-Many: Course -> Materials
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Materials)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-Many: User <-> Skill (via UserSkill)
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

            // Many-to-Many: Course <-> Materials
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Materials);                

            // Many-to-Many: Course <-> Skills
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
