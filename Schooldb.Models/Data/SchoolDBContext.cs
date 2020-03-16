using Microsoft.EntityFrameworkCore;
using Schooldb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.Models
{
    public class SchoolDBContext : DbContext
    {
        public SchoolDBContext(DbContextOptions<SchoolDBContext> options):base(options)
        {

        }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeachersEducations> TeachersEducations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Identity kolom
            //modelBuilder.Entity<Education>().Property(e => e.Id).UsePropertyAccessMode(PropertyAccessMode);

            // Tussentabel = dubbele PK aanmaken!!!
            /*modelBuilder.Entity<TeachersEducations>(entity =>
            {
                //entity.HasKey(e =>new (e.EducationId, e.TeacherId));
                entity.HasKey(e => new { e.TeacherId, e.EducationId });
            });*/

            //tussentabel = dubbele P uitwerken 
            modelBuilder.Entity<TeachersEducations>()
                 .HasKey(t => new { t.TeacherId, t.EducationId });

            //Identity kolom --> Moet er niet staan
            //modelBuilder.Entity<Education>().Property(e => e.Id).UseIdentityColumn();

        }
    }
}
