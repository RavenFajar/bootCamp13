using Microsoft.EntityFrameworkCore;
using DepartmentPortal.Models.Entities;
namespace DepartmentPortal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var Departments = new List<Department>
        {
            new Department { Id = Guid.NewGuid(), Name = "HR" },
            new Department { Id = Guid.NewGuid(), Name = "IT" },
            new Department { Id = Guid.NewGuid(), Name = "Finance" },
            new Department { Id = Guid.NewGuid(), Name = "Marketing" },
            new Department { Id = Guid.NewGuid(), Name = "Sales" }
        };
        var Employees = new List<Employee>
        {
            new Employee { Id = Guid.NewGuid(), HireDate = DateTime.Now, Name = "Alice", Position = "HR", Salary = 60000 },
            new Employee { Id = Guid.NewGuid(), HireDate = DateTime.Now, Name = "Bob", Position = "IT", Salary = 70000 },
            new Employee { Id = Guid.NewGuid(), HireDate = DateTime.Now, Name = "Charlie", Position = "Finance", Salary = 80000 },
            new Employee { Id = Guid.NewGuid(), HireDate = DateTime.Now, Name = "David", Position = "Marketing", Salary = 50000 },
            new Employee { Id = Guid.NewGuid(), HireDate = DateTime.Now, Name = "Eve", Position = "Sales", Salary = 55000 },
        };
        modelBuilder.Entity<Department>().HasData(Departments);
        modelBuilder.Entity<Employee>().HasData(Employees);
    }
}