using EmployeeAdmin.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdmin.Data
{
    public class ApplicationtDBContext : DbContext
    {
        public ApplicationtDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

    }
}
