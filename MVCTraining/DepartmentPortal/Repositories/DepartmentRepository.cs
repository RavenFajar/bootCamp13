using DepartmentPortal.Data;
using DepartmentPortal.Models.Entities;
using DepartmentPortal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DepartmentPortal.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Department> AddAsync(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            return department;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Department?> UpdateAsync(Department department)
        {
            var existingDepartment = await _dbContext.Departments.FindAsync(department.Id);
            if (existingDepartment == null)
            {
                return null;
            }

            _dbContext.Entry(existingDepartment).CurrentValues.SetValues(department);
            return existingDepartment;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var department = await _dbContext.Departments.FindAsync(id);
                if (department == null) 
                    return false;

                _dbContext.Departments.Remove(department);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Departments.AnyAsync(d => d.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}