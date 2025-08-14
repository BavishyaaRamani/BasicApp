using System;
using BasicApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApp.Database
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }

}

