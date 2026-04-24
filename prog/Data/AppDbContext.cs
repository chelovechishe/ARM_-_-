using ARM_Отдела_кадров.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM_Отдела_кадров.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PersonnelEvent> PersonnelEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HRDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Status)
                .HasDefaultValue("Работает");

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionID);

            modelBuilder.Entity<PersonnelEvent>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.PersonnelEvents)
                .HasForeignKey(p => p.EmployeeID);

            modelBuilder.Entity<PersonnelEvent>()
                .HasOne(p => p.Position)
                .WithMany(po => po.PersonnelEvents)
                .HasForeignKey(p => p.PositionID);

            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Login = "Admin", Password = "123", Role = "Administrator" },
                new User { UserID = 2, Login = "User", Password = "123", Role = "User" }
            );
            modelBuilder.Entity<Position>().HasData(
                new Position { PositionID = 1, PositionName = "Директор", Salary = 150000 },
                new Position { PositionID = 2, PositionName = "Главный бухгалтер", Salary = 120000 },
                new Position { PositionID = 3, PositionName = "Программист", Salary = 80000 },
                new Position { PositionID = 4, PositionName = "Менеджер по персоналу", Salary = 70000 },
                new Position { PositionID = 5, PositionName = "Секретарь", Salary = 50000 }
            );
        }
    }
}
