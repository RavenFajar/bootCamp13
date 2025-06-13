namespace DepartmentPortal.Models.Entities
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public ICollection<Employee>? Employees { get; set; }
    }
}