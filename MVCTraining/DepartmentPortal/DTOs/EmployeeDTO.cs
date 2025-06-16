using DepartmentPortal.Models.Entities;

namespace DepartmentPortal.DTOs;

public class EmployeeDto{

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public Department? Department { get; set; }
}

