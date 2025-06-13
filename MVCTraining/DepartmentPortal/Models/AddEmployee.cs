using System;
using DepartmentPortal.Models.Entities;
namespace DepartmentPortal.Models;

public class AddEmployee
{
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; }

}