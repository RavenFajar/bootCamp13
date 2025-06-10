namespace StudentPortal.Models
{
    public class AddStudentsModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}