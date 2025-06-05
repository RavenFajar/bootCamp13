namespace EmployeeAdmin.Models.Entities
{
    public class Employee
    {   
        public Guid Id { get; set; } = Guid.NewGuid(); // Using Guid for unique identifier
        //public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public decimal Salary { get; set; }
    }
}
