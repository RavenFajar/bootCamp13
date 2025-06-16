namespace DepartmentPortal.DTOs;
public class DepartmentDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Location { get; set; } = string.Empty;

    // You can add validation attributes if needed, e.g.:
    // [Required]
    // [StringLength(100)]
    // public string DepartmentName { get; set; }
}