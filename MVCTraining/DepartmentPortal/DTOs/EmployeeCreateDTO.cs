using DepartmentPortal.Models.Entities;
namespace DepartmentPortal.DTOs;

public class EmployeeCreateDto {
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; }
}