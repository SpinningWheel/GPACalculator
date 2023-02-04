using GPACalculator.Entities;
using Microsoft.EntityFrameworkCore;

namespace GPACalculator.Data {
    public class AppDbContext : DbContext {
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students{ get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) {
                        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}
