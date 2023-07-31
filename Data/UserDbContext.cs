using Employees.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
