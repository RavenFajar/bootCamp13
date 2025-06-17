using System.ComponentModel.DataAnnotations;

namespace DepartmentPortal.Models.Entities
{
    public class Department
    {
        public Guid Id { get; set; }
        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Department Location")]
        public string Location { get; set; } = string.Empty;
        public ICollection<Employee>? Employees { get; set; }
    }
}