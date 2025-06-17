using DepartmentPortal.Models.Entities;

namespace DepartmentPortal.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> AddAsync(Department department);
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(Guid id);
        Task<Department?> UpdateAsync(Department department);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task SaveChangesAsync();
    }
}