using StudentPortal.Models.Entities;

namespace StudentPortal.Models.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Subject> Subject { get; set; } = new List<Subject>();
        public ICollection<Student> Student { get; set; } = new List<Student>();

    }
}