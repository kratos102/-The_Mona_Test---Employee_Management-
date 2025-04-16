using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EmployeeManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EmployeeCode)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(e => e.FullName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.DateOfBirth)
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}