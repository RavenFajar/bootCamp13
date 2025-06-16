namespace DepartmentPortal.DTOs;
public class DepartmentCreateDto
{
    public required string DepartmentName { get; set; }
    public string Location { get; set; } = string.Empty;

}