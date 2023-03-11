


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

        public DbSet<Result> Results { get; set; }

        public DbSet<Note> Notes { get; set; }
        
    }
}
