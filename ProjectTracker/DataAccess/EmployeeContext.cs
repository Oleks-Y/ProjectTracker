using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjectTracker.Models;

namespace ProjectTracker.DataAccess
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
          
        }
        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // Todo Project not showing in Activity !
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Activities)
                .WithOne(e => e.Employee)
                .OnDelete(DeleteBehavior.Cascade);

            
            

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Project)
                .WithOne(p => p.Activity)
                .HasForeignKey<Project>(p => p.ActivityForeignKey);
            
            
            
            



        }
    }
}