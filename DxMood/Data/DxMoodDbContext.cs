


using IO.Swagger.Models;
using Microsoft.EntityFrameworkCore;

namespace DxMood.Data
{
    public class DxMoodDbContext : DbContext
    {
        public DxMoodDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Patients)
                .HasForeignKey(p => p.DoctorId);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Result)
                .WithMany(r => r.Notes)
                .HasForeignKey(n => n.ResultId);
        }
    }
}
