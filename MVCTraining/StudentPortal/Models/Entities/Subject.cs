namespace StudentPortal.Models.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property for related students
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}