namespace DepartmentPortal.DTOs;
public class DepartmentDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Location { get; set; } = string.Empty;

}