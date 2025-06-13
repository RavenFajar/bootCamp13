using Microsoft.EntityFrameworkCore;
using StudentPortal.Models.Entities;
namespace StudentPortal.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subject { get; set; }
    public DbSet<Enrollment> Enrollment { get; set; }

}