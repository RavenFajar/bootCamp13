namespace StudentPortal.Models
{
    public class AddSubjectModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
    }
}